using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class FolderService : IFolderService
    {
        public const string RootPath = @"C:\Users\Vlad\Desktop\Core\_FileStorage\FileStorageFront\src\assets\Content";
        private readonly IUnitOfWork _data;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public FolderService(IUnitOfWork data, IFileService fileService, IUserService userService)
        {
            _data = data;
            _fileService = fileService;
            _userService = userService;
        }

        //Ok
        public HashSet<FolderDto> GetAll()
        {
            return Mapper.Map<HashSet<FolderDto>>(_data.Folders.GetAll());
        }

        public List<FolderDto> GetAllRootFolders()
        {
            return GetAll().Where(f => f.ParentFolderId == null).ToList();
        }

        //Ok
        public FolderDto GetRootFolderContentByUserId(string userId)
        {
            return Mapper.Map<FolderDto>(_data.Folders.GetAll().FirstOrDefault(f => f.UserId.Equals(userId))
                                         ?? throw new FolderNotFoundException(
                                             $"Cannot find root folder with userId = {userId}"));
        }

        //Ok
        public FolderDto Get(int id)
        {
            return Mapper.Map<FolderDto>(_data.Folders.Get(id));
        }

        //Ok
        public FolderDto GetByUserId(int id, string userId)
        {
            var folder = Mapper.Map<FolderDto>(_data.Folders.Get(id));
            return folder.UserId.Equals(userId)
                ? folder
                : throw new FolderNotFoundException($"Cannot find folder with id = {id} and userId = {userId}");
        }

        //Ok
        public void Create(FolderDto item)
        {
            _data.Folders.Create(Mapper.Map<UserFolder>(item));
            Directory.CreateDirectory(ReturnFullFolderPath(item));
            _data.Save();
        }

        //Ok
        public void EditFolder(int id, FolderDto item)
        {
            var folder = _data.Folders.Get(id);
            var oldPath = ReturnFolderPath(Mapper.Map<FolderDto>(folder));
            folder.Name = item.Name;
            var newPath = ReturnFolderPath(Mapper.Map<FolderDto>(folder));
            Directory.Move(RootPath + oldPath, RootPath + newPath);
            _data.Folders.Update(folder);

            _data.Save();
        }

        //Ok
        public void Delete(FolderDto folderDto)
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
        public FolderDto CreateRootFolder(string userId, string email)
        {
            var folder = new FolderDto
            {
                Name = email,
                Path = "",
                UserId = email
            };
            Create(folder);
            return folder;
        }

        //Ok
        public FolderDto CreateFolderInFolder(FolderDto parent, string name)
        {
            var folder = new FolderDto
            {
                Name = name,
                Path = ReturnFolderPath(parent),
                ParentFolderId = parent.Id,
                UserId = parent.UserId
            };
            if (IsFolderExists(folder))
                throw new FolderWrongNameException(
                    "The folder with the specified name exists. Please change the folder name");
            Create(folder);
            return folder;
        }

        //Ok
        public bool IsFolderExists(FolderDto file)
        {
            return Directory.Exists(ReturnFullFolderPath(file));
        }

        //Ok
        public void Dispose()
        {
            _data.Dispose();
        }

        public bool CanAdd(string email, long itemSize)
        {
            var folderSize = GetRootFolderSize(email);
            var userMemorySize = _userService.GetByEmail(email).MemorySize;
            return userMemorySize - folderSize - itemSize > 0;
        }

        public long GetRootFolderSize(string email)
        {
            var folder = GetRootFolderContentByUserId(email);
            return GetRootFolderSizeByPath(ReturnFullFolderPath(folder));
        }

        //Ok
        private string ReturnFolderPath(FolderDto item)
        {
            return item.Path + @"\" + item.Name;
        }

        //Ok
        private string ReturnFullFolderPath(FolderDto item)
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