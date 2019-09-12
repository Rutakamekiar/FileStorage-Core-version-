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

        //Ok
        public HashSet<Folder> GetAll()
        {
            return _mapper.Map<HashSet<Folder>>(_data.Folders.GetAll());
        }

        public List<Folder> GetAllRootFolders()
        {
            return GetAll().Where(f => f.ParentFolderId == null).ToList();
        }

        //Ok
        public Folder Get(Guid id)
        {
            return _mapper.Map<Folder>(_data.Folders.Get(id));
        }

        //Ok
        public void Create(Folder item)
        {
            _data.Folders.Create(_mapper.Map<FolderEntity>(item));
            Directory.CreateDirectory(ReturnFullFolderPath(item));
            _data.Save();
        }

        //Ok
        public void EditFolder(Guid id, Folder item)
        {
            var folder = _data.Folders.Get(id);
            var oldPath = ReturnFolderPath(_mapper.Map<Folder>(folder));
            folder.Name = item.Name;
            var newPath = ReturnFolderPath(_mapper.Map<Folder>(folder));
            Directory.Move(RootPath + oldPath, RootPath + newPath);
            _data.Folders.Update(folder);

            _data.Save();
        }

        //Ok
        public void Delete(Folder folderDto)
        {
            if (!folderDto.Files.Count.Equals(0))
                foreach (var file in folderDto.Files)
                    _fileService.Delete(file);

            if (!folderDto.Folders.Count.Equals(0))
                foreach (var folder in folderDto.Folders)
                    Delete(folder);

            _data.Folders.Delete(folderDto.Id);
            Directory.Delete(ReturnFullFolderPath(folderDto));
            _data.Save();
        }

        //Ok
        public Folder CreateFolderInFolder(Folder parent, string name)
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
            Create(folder);
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
            var folderSize = GetRootFolderSize(userId);
            var userMemorySize = await _userService.GetMemorySize(userId);
            return userMemorySize - folderSize - itemSize > 0;
        }

        public long GetRootFolderSize(string userId)
        {
            var folder = GetRootFolderContentByUserId(userId);
            return GetRootFolderSizeByPath(ReturnFullFolderPath(folder));
        }

        //Ok
        public Folder GetRootFolderContentByUserId(string userId)
        {
            return _mapper.Map<Folder>(_data.Folders.GetAll().FirstOrDefault(f => f.UserId.Equals(userId))
                                         ?? throw new FolderNotFoundException(
                                             $"Cannot find root folderEntity with userId = {userId}"));
        }

        //Ok
        public Folder GetByUserId(Guid id, string userId)
        {
            var folder = _mapper.Map<Folder>(_data.Folders.Get(id));
            return folder.UserId.Equals(userId)
                ? folder
                : throw new FolderNotFoundException($"Cannot find folderEntity with id = {id} and userId = {userId}");
        }

        //Ok
        public Folder CreateRootFolder(string userId, string email)
        {
            var folder = new Folder
            {
                Name = email,
                Path = "",
                UserId = userId
            };
            Create(folder);
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