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
using FileStorage.Contracts.DTO;
using FileStorage.Contracts.Requests;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.Exceptions;
using FileStorage.Implementation.Options;
using FileStorage.Implementation.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
                             IOptions<PathOptions> pathOptions)
        {
            _data = data;
            _fileService = fileService;
            _userService = userService;
            _mapper = mapper;
            _physicalFolderService = physicalFolderService;
            _physicalFileService = physicalFileService;
            _rootPath = pathOptions.Value.RootPath;
        }

        public async Task<IEnumerable<Folder>> GetAllRootFoldersAsync()
        {
            return _mapper.Map<IEnumerable<Folder>>(await _data.Folders.GetByCondition(f => f.ParentFolderId == null).ToArrayAsync());
        }

        public async Task<Folder> GetByIdAsync(Guid id)
        {
            var folder = await _data.Folders.GetByIdAsync(id);
            return _mapper.Map<Folder>(folder);
        }

        public async Task<Guid> CreateAsync(Folder item)
        {
            var folderEntity = _mapper.Map<FolderEntity>(item);
            await _data.Folders.CreateAsync(folderEntity);
            var path = ReturnFullFolderPath(item);
            _physicalFolderService.CreateFolder(path);
            await _data.SaveChangesAsync();
            return folderEntity.Id;
        }

        public async Task UpdateFolderAsync(Guid id, Guid userId, UpdateFolderRequest item)
        {
            var folderEntity = await _data.Folders.GetByIdAsync(id);
            if (folderEntity.UserId != userId)
            {
                throw new FolderNotFoundException(id.ToString());
            }

            var parentFolder = await _data.Folders.GetByIdAsync(item.ParentFolderId);
            var oldPath = _rootPath + ReturnFolderPath(folderEntity);
            var newPath = _rootPath + ReturnFolderPath(parentFolder) + @"\" + item.Name;
            _physicalFolderService.ReplaceFolder(oldPath, newPath);

            folderEntity.ParentFolderId = item.ParentFolderId;
            folderEntity.Name = item.Name;
            _data.Folders.Update(parentFolder);

            await _data.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, Guid userId)
        {
            var folderEntity = await _data.Folders.GetByIdAsync(id);
            if (folderEntity.UserId != userId)
            {
                throw new FolderNotFoundException(id.ToString());
            }

            foreach (var file in folderEntity.Files)
            {
                await _fileService.DeleteAsync(file.Id, userId);
            }

            foreach (var folder in folderEntity.Folders)
            {
                await DeleteAsync(folder.Id, userId);
            }

            await _data.Folders.DeleteByIdAsync(folderEntity.Id);
            _physicalFolderService.DeleteFolder(ReturnFullFolderPath(folderEntity));
            await _data.SaveChangesAsync();
        }

        public async Task<Guid> CreateFolderInFolderAsync(Folder parent, string name)
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

            return await CreateAsync(folder);
        }

        public async Task<bool> CanAddAsync(Guid userId, long itemSize)
        {
            var folderSize = await GetSpaceUsedCountByUserId(userId);
            var userMemorySize = await _userService.GetMemorySizeByUserIdAsync(userId);
            return userMemorySize - folderSize - itemSize > 0;
        }

        public async Task<long> GetSpaceUsedCountByUserId(Guid userId)
        {
            var folder = await GetRootFolderByUserIdAsync(userId);
            return GetRootFolderSizeByPath(ReturnFullFolderPath(folder));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        public async Task<Folder> GetRootFolderByUserIdAsync(Guid userId)
        {
            var folderEntity = await _data.Folders
                                          .GetAll()
                                          .SingleOrDefaultAsync(x => x.UserId.Equals(userId) && x.ParentFolderId == null);
            return _mapper.Map<Folder>(folderEntity);
        }

        public async Task<Folder> GetByUserIdAsync(Guid id, Guid userId)
        {
            var folder = _mapper.Map<Folder>(await _data.Folders.GetByIdAsync(id));
            return folder.UserId == userId
                ? folder
                : throw new FolderNotFoundException(id.ToString());
        }

        public async Task<Folder> CreateRootFolder(Guid userId, string email)
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

        private static string ReturnFolderPath(FolderEntity item)
        {
            return item.Path + @"\" + item.Name;
        }

        private string ReturnFullFolderPath(Folder item)
        {
            return _rootPath + ReturnFolderPath(item);
        }

        private string ReturnFullFolderPath(FolderEntity item)
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