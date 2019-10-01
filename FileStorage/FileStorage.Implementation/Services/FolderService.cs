using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileStorage.Contracts;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.Exceptions;
using FileStorage.Implementation.Interfaces;

namespace FileStorage.Implementation.Services
{
    public class FolderService : IFolderService
    {
        public const string RootPath = @"C:\Users\Vlad\Desktop\Core\_FileStorage\FileStorageFront\src\assets\Content";
        private readonly IUnitOfWork _data;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FolderService(IUnitOfWork data,
                             IFileService fileService,
                             IUserService userService,
                             IMapper mapper)
        {
            _data = data;
            _fileService = fileService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<HashSet<Folder>> GetAllAsync()
        {
            return _mapper.Map<HashSet<Folder>>(await _data.Folders.GetAllAsync());
        }

        public async Task<List<Folder>> GetAllRootFolders()
        {
            return (await GetAllAsync()).Where(f => f.ParentFolderId == null).ToList();
        }

        public Folder Get(Guid id)
        {
            return _mapper.Map<Folder>(_data.Folders.GetAsync(id));
        }

        public async Task CreateAsync(Folder item)
        {
            await _data.Folders.CreateAsync(_mapper.Map<FolderEntity>(item));
            Directory.CreateDirectory(ReturnFullFolderPath(item));
            await _data.SaveAsync();
        }

        public async Task EditFolder(Guid id, Folder item)
        {
            var folder = await _data.Folders.GetAsync(id);
            var oldPath = ReturnFolderPath(_mapper.Map<Folder>(folder));
            folder.Name = item.Name;
            var newPath = ReturnFolderPath(_mapper.Map<Folder>(folder));
            Directory.Move(RootPath + oldPath, RootPath + newPath);
            _data.Folders.Update(folder);

            await _data.SaveAsync();
        }

        public async Task DeleteAsync(Folder folderDto)
        {
            if (!folderDto.Files.Count.Equals(0))
                foreach (var file in folderDto.Files)
                    await _fileService.DeleteAsync(file);

            if (!folderDto.Folders.Count.Equals(0))
                foreach (var folder in folderDto.Folders)
                    await DeleteAsync(folder);

            await _data.Folders.DeleteAsync(folderDto.Id);
            Directory.Delete(ReturnFullFolderPath(folderDto));
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
            if (IsFolderExists(folder))
                throw new FolderWrongNameException(
                    "The folderEntity with the specified name exists. Please change the folderEntity name");
            await CreateAsync(folder);
            return folder;
        }

        //Ok
        public bool IsFolderExists(Folder file)
        {
            return Directory.Exists(ReturnFullFolderPath(file));
        }

        //Ok
        public void Dispose()
        {
            _data.Dispose();
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
            return _mapper.Map<Folder>((await _data.Folders.GetAllAsync()).FirstOrDefault(f => f.UserId.Equals(userId))
                                         ?? throw new FolderNotFoundException(
                                             $"Cannot find root folderEntity with userId = {userId}"));
        }

        public Folder GetByUserId(Guid id, string userId)
        {
            var folder = _mapper.Map<Folder>(_data.Folders.GetAsync(id));
            return folder.UserId.Equals(userId)
                ? folder
                : throw new FolderNotFoundException($"Cannot find folderEntity with id = {id} and userId = {userId}");
        }

        //Ok
        public async Task<Folder> CreateRootFolder(string userId, string email)
        {
            var folder = new Folder
            {
                Name = email,
                Path = "",
                UserId = userId
            };
            await CreateAsync(folder);
            return folder;
        }

        //Ok
        private string ReturnFolderPath(Folder item)
        {
            return item.Path + @"\" + item.Name;
        }

        //Ok
        private string ReturnFullFolderPath(Folder item)
        {
            return RootPath + ReturnFolderPath(item);
        }

        private long GetRootFolderSizeByPath(string path)
        {
            long sum = 0;
            var dirs = Directory.GetDirectories(path);
            if (dirs.Length > 0)
                foreach (var s in dirs)
                    sum += GetRootFolderSizeByPath(s);
            var files = Directory.GetFiles(path);
            if (files.Length > 0)
                foreach (var s in files)
                {
                    var fileInfo = new FileInfo(s);
                    sum += fileInfo.Length;
                }

            return sum;
        }
    }
}