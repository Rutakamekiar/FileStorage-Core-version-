using System.Collections.Generic;
using BLL.DTO;
using BLL.Interfaces;
using System.IO;
using System.Linq;
using DAL.Interfaces;
using AutoMapper;
using DAL.Entities;
using System;

namespace BLL.Services
{
    public class FolderService : IFolderService
    {
        private readonly IUnitOfWork _data;
        private readonly IFileService _fileService;
        private readonly IUserService _userSevice;

        public const string RootPath = @"C:\Users\Vlad\Desktop\Core\_FileStorage\FileStorageFront\src\assets\Content";

        public FolderService(IUnitOfWork data, IFileService fileService, IUserService userSevice)
        {
            _data = data;
            _fileService = fileService;
            _userSevice = userSevice;
        }

        //Ok
        public HashSet<FolderDTO> GetAll()
        {
            return Mapper.Map<HashSet<FolderDTO>>(_data.Folders.GetAll());
        }

        public List<FolderDTO> GetAllRootFolders()
        {
            return GetAll().Where(f => f.ParentFolderId == null).ToList();
        }

        //Ok
        public FolderDTO GetRootFolderContentByUserId(string userId)
        {
            return Mapper.Map<FolderDTO>(
                _data.Folders.GetAll()
                    .Where(f => f.UserId.Equals(userId))
                    .FirstOrDefault()
                ?? throw new FolderNotFoundException($"Cannot find root folder with userId = {userId}"));
        }

        //Ok
        public FolderDTO Get(int id)
        {
            return Mapper.Map<FolderDTO>(_data.Folders.Get(id));
        }

        //Ok
        public FolderDTO GetByUserId(int id, string userId)
        {
            var folder = Mapper.Map<FolderDTO>(_data.Folders.Get(id));
            return folder.UserId.Equals(userId)
                ? folder
                : throw new FolderNotFoundException($"Cannot find folder with id = {id} and userId = {userId}");
        }

        //Ok
        public void Create(FolderDTO item)
        {
            _data.Folders.Create(Mapper.Map<UserFolder>(item));
            Directory.CreateDirectory(ReturnFullFolderPath(item));
            _data.Save();
        }

        //Ok
        private string ReturnFolderPath(FolderDTO item)
        {
            return item.Path + @"\" + item.Name;
        }

        //Ok
        private string ReturnFullFolderPath(FolderDTO item)
        {
            return RootPath + ReturnFolderPath(item);
        }

        //Ok
        public void EditFolder(int id, FolderDTO item)
        {
            var folder = _data.Folders.Get(id);
            string oldPath = ReturnFolderPath(Mapper.Map<FolderDTO>(folder));
            folder.Name = item.Name;
            string newPath = ReturnFolderPath(Mapper.Map<FolderDTO>(folder));
            Directory.Move(RootPath + oldPath, RootPath + newPath);
            _data.Folders.Update(folder);

            _data.Save();
        }

        //Ok
        public void Delete(FolderDTO folderDto)
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
        public FolderDTO CreateRootFolder(string userId, string email)
        {
            FolderDTO folder = new FolderDTO()
            {
                Name = email,
                Path = "",
                UserId = email,
            };
            Create(folder);
            return folder;
        }

        //Ok
        public FolderDTO CreateFolderInFolder(FolderDTO parent, string name)
        {
            FolderDTO folder = new FolderDTO()
            {
                Name = name,
                Path = ReturnFolderPath(parent),
                ParentFolderId = parent.Id,
                UserId = parent.UserId,
            };
            if (IsFolderExists(folder))
            {
                throw new FolderWrongNameException("The folder with the specified name exists. Please change the folder name");
            }
            Create(folder);
            return folder;
        }

        //Ok
        public bool IsFolderExists(FolderDTO file)
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
            long folderSize = GetRootFolderSize(email);
            long userMemorySize = _userSevice.GetByEmail(email).MemorySize;
            return userMemorySize - folderSize - itemSize > 0;
        }

        private long GetRootFolderSizeByPath(string path)
        {
            long sum = 0;
            string[] dirs = Directory.GetDirectories(path);
            if (dirs.Length > 0)
            {
                foreach (string s in dirs)
                {
                    sum += GetRootFolderSizeByPath(s);
                }
            }
            string[] files = Directory.GetFiles(path);
            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    FileInfo finfo = new FileInfo(s);
                    sum += finfo.Length;
                }
            }
            return sum;
        }

        public long GetRootFolderSize(string email)
        {
            FolderDTO folder = GetRootFolderContentByUserId(email);
            return GetRootFolderSizeByPath(ReturnFullFolderPath(folder));
        }
    }
}