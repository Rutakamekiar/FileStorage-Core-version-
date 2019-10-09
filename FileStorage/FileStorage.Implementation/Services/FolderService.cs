// <copyright file="FolderService.cs" company="Kovalov Systems">
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
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.Exceptions;
using FileStorage.Implementation.Interfaces;
using FileStorage.Implementation.Options;

namespace FileStorage.Implementation.Services
{
    public class FolderService : IFolderService
    {
        private readonly string _rootPath;
        private readonly IUnitOfWork _data;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPhysicalFolderService _physicalFolderService;
        private readonly IPhysicalFileService _physicalFileService;

        public FolderService(IUnitOfWork data,
                             IFileService fileService,
                             IUserService userService,
                             IMapper mapper,
                             IPhysicalFolderService physicalFolderService,
                             IPhysicalFileService physicalFileService,
                             PathOptions pathOptions)
        {
            _data = data;
            _fileService = fileService;
            _userService = userService;
            _mapper = mapper;
            _physicalFolderService = physicalFolderService;
            _physicalFileService = physicalFileService;
            _rootPath = pathOptions.RootPath;
        }

        public async Task<HashSet<Folder>> GetAllAsync()
        {
            return _mapper.Map<HashSet<Folder>>(await _data.Folders.GetAllAsync());
        }

        public async Task<List<Folder>> GetAllRootFolders()
        {
            return (await GetAllAsync()).Where(f => f.ParentFolderId == null).ToList();
        }

        public Folder GetItem(Guid id)
        {
            return _mapper.Map<Folder>(_data.Folders.GetAsync(id));
        }

        public async Task CreateAsync(Folder item)
        {
            await _data.Folders.CreateAsync(_mapper.Map<FolderEntity>(item));
            var path = ReturnFullFolderPath(item);
            _physicalFolderService.CreateFolder(path);
            await _data.SaveAsync();
        }

        public async Task EditFolder(Guid id, Folder item)
        {
            var folder = await _data.Folders.GetAsync(id);
            var oldPath = _rootPath + ReturnFolderPath(_mapper.Map<Folder>(folder));
            folder.Name = item.Name;
            var newPath = _rootPath + ReturnFolderPath(_mapper.Map<Folder>(folder));
            _physicalFolderService.ReplaceFolder(oldPath, newPath);
            _data.Folders.Update(folder);

            await _data.SaveAsync();
        }

        public async Task DeleteAsync(Folder item)
        {
            if (!item.Files.Count.Equals(0))
            {
                foreach (var file in item.Files)
                {
                    await _fileService.DeleteAsync(file);
                }
            }

            if (!item.Folders.Count.Equals(0))
            {
                foreach (var folder in item.Folders)
                {
                    await DeleteAsync(folder);
                }
            }

            await _data.Folders.DeleteAsync(item.Id);
            _physicalFolderService.DeleteFolder(ReturnFullFolderPath(item));
            await _data.SaveAsync();
        }

        public async Task<Folder> CreateFolderInFolder(Folder parent, string name)
        {
            var folder = new Folder
            {
                Name = name,
                Path = ReturnFolderPath(parent),
                ParentFolderId = parent.Id,
                UserId = parent.UserId
            };
            if (_physicalFolderService.CheckFolder(ReturnFullFolderPath(folder)))
            {
                throw new FolderWrongNameException();
            }

            await CreateAsync(folder);
            return folder;
        }

        public async Task<bool> CanAddAsync(string userId, long itemSize)
        {
            var folderSize = await GetRootFolderSize(userId);
            var userMemorySize = await _userService.GetMemorySize(userId);
            return userMemorySize - folderSize - itemSize > 0;
        }

        public async Task<long> GetRootFolderSize(string userId)
        {
            var folder = await GetRootFolderContentByUserId(userId);
            return GetRootFolderSizeByPath(ReturnFullFolderPath(folder));
        }

        public async Task<Folder> GetRootFolderContentByUserId(string userId)
        {
            var folderEntity = (await _data.Folders.GetAllAsync()).FirstOrDefault(f => f.UserId == userId);
            return _mapper.Map<Folder>(folderEntity);
        }

        public Folder GetByUserId(Guid id, string userId)
        {
            var folder = _mapper.Map<Folder>(_data.Folders.GetAsync(id));
            return folder.UserId == userId
                ? folder
                : throw new FolderNotFoundException(id.ToString());
        }

        public async Task<Folder> CreateRootFolder(string userId, string email)
        {
            var folder = new Folder
            {
                Name = email,
                Path = string.Empty,
                UserId = userId
            };
            await CreateAsync(folder);
            return folder;
        }

        private static string ReturnFolderPath(Folder item)
        {
            return item.Path + @"\" + item.Name;
        }

        private string ReturnFullFolderPath(Folder item)
        {
            return _rootPath + ReturnFolderPath(item);
        }

        private long GetRootFolderSizeByPath(string path)
        {
            long sum = 0;
            var dirs = _physicalFolderService.GetFolders(path);
            if (dirs.Length > 0)
            {
                sum += dirs.Sum(GetRootFolderSizeByPath);
            }

            var filesPath = _physicalFolderService.GetFolderFiles(path);
            if (filesPath.Length > 0)
            {
                sum += filesPath.Sum(filePath => _physicalFileService.GetFileLength(filePath));
            }

            return sum;
        }
    }
}