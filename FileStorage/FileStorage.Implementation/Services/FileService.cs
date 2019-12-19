// <copyright file="FileService.cs" company="Kovalov Systems">
// Confidential and Proprietary
// Copyright 2019 Kovalov Systems
// ALL RIGHTS RESERVED.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts.DTO;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.Options;
using FileStorage.Implementation.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
                           IOptions<PathOptions> pathOptions)
        {
            _data = data;
            _mapper = mapper;
            _physicalFileService = physicalFileService;
            _rootPath = pathOptions.Value.RootPath;
        }

        public async Task<Guid> CreateAsync(MyFile item)
        {
            var fileEntity = _mapper.Map<FileEntity>(item);
            await _data.Files.CreateAsync(fileEntity);
            var path = await ReturnFullPathAsync(item);
            _physicalFileService.CreateFile(path, item.FileBytes);
            await _data.SaveChangesAsync();
            return fileEntity.Id;
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

        public async Task DeleteAsync(Guid id)
        {
            var fileEntity = await _data.Files.GetByIdAsync(id);
            await _data.Files.DeleteByIdAsync(id);
            var path = await ReturnFullPathAsync(fileEntity);
            _physicalFileService.DeleteFile(path);
            await _data.SaveChangesAsync();
        }

        public async Task UpdateFileAsync(Guid id, MyFile file)
        {
            var newFile = await _data.Files.GetByIdAsync(id);
            var oldPath = await ReturnFullPathAsync(_mapper.Map<MyFile>(newFile));
            newFile.Name = file.Name;
            var newPath = await ReturnFullPathAsync(_mapper.Map<MyFile>(newFile));
            _physicalFileService.ReplaceFile(oldPath, newPath);

            newFile.AccessLevel = file.AccessLevel;
            newFile.IsBlocked = file.IsBlocked;
            _data.Files.Update(newFile);
            await _data.SaveChangesAsync();
        }

        public async Task<bool> IsFileExistsAsync(MyFile file)
        {
            return _physicalFileService.CheckFile(await ReturnFullPathAsync(file));
        }

        public IEnumerable<MyFile> GetAllByUserId(Guid userid)
        {
            return _mapper.Map<IEnumerable<MyFile>>(_data.Files.GetByCondition(f => f.Folder.UserId == userid));
        }

        private async Task<string> ReturnFullPathAsync(FileEntity file)
        {
            var folder = await _data.Folders.GetByIdAsync(file.FolderId);
            return $@"{_rootPath}\{folder.Path}\{folder.Name}\{file.Name}";
        }
    }
}