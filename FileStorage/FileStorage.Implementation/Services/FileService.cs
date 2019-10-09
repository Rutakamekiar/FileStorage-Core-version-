// <copyright file="FileService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts;
using FileStorage.Contracts.DTO;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.Interfaces;
using FileStorage.Implementation.Options;

namespace FileStorage.Implementation.Services
{
    public class FileService : IFileService
    {
        private readonly string _rootPath;
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly IPhysicalFileService _physicalFileService;

        public FileService(IUnitOfWork data,
                           IMapper mapper,
                           IPhysicalFileService physicalFileService,
                           PathOptions pathOptions)
        {
            _data = data;
            _mapper = mapper;
            _physicalFileService = physicalFileService;
            _rootPath = pathOptions.RootPath;
        }

        public async Task CreateAsync(MyFile item)
        {
            await _data.Files.CreateAsync(_mapper.Map<FileEntity>(item));
            var path = await ReturnFullPathAsync(item);
            _physicalFileService.CreateFile(path, item.FileBytes);
            await _data.SaveAsync();
        }

        public async Task<string> ReturnFullPathAsync(MyFile file)
        {
            var folder = await _data.Folders.GetByIdAsync(file.FolderId);
            return $@"{_rootPath}\{folder.Path}\{folder.Name}\{file.Name}";
        }

        public async Task<MyFile> GetByIdAsync(Guid id)
        {
            return _mapper.Map<MyFile>(await _data.Files.GetByIdAsync(id));
        }

        public async Task<byte[]> GetFileBytesAsync(MyFile fileDto)
        {
            return _physicalFileService.ReadFile(await ReturnFullPathAsync(fileDto));
        }

        public async Task DeleteAsync(MyFile item)
        {
            await _data.Files.DeleteByIdAsync(item.Id);
            var path = await ReturnFullPathAsync(item);
            _physicalFileService.DeleteFile(path);
            await _data.SaveAsync();
        }

        public IEnumerable<MyFile> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<MyFile>>(_data.Files.GetAll());
        }

        public async Task EditFileAsync(Guid id, MyFile fileDto)
        {
            var newFile = await _data.Files.GetByIdAsync(id);
            var oldPath = await ReturnFullPathAsync(_mapper.Map<MyFile>(newFile));
            newFile.Name = fileDto.Name;
            var newPath = await ReturnFullPathAsync(_mapper.Map<MyFile>(newFile));
            _physicalFileService.ReplaceFile(oldPath, newPath);

            newFile.AccessLevel = fileDto.AccessLevel;
            newFile.IsBlocked = fileDto.IsBlocked;
            _data.Files.Update(newFile);
            await _data.SaveAsync();
        }

        public async Task<bool> IsFileExistsAsync(MyFile file)
        {
            return _physicalFileService.CheckFile(await ReturnFullPathAsync(file));
        }

        public IEnumerable<MyFile> GetAllByUserIdAsync(string userid)
        {
            return _mapper.Map<IEnumerable<MyFile>>(_data.Files.GetByCondition(f => f.Folder.UserId == userid));
        }
    }
}