using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class FileService : IFileService, IDisposable
    {
        private readonly IUnitOfWork _data;
        private const string RootFilePath = @"C:\Users\Vlad\Desktop\Core\_FileStorage\FileStorageFront\src\assets\Content";

        public FileService(IUnitOfWork data)
        {
            _data = data;
        }

        //Ok
        public void Create(FileDTO item)
        {
            _data.Files.Create(Mapper.Map<UserFile>(item));
            File.WriteAllBytes(ReturnFullPath(item), item.FileBytes);

            _data.Save();
        }

        //Ok
        public string ReturnFullPath(FileDTO file)
        {
            UserFolder folder = _data.Folders.Get(file.FolderId);
            return $@"{RootFilePath}\{folder.Path}\{folder.Name}\{file.Name}";
        }

        //Ok
        public FileDTO Get(int id)
        {
            return Mapper.Map<FileDTO>(_data.Files.Get(id));
        }

        //Ok
        public byte[] GetFileBytes(FileDTO fileDto)
        {
            return File.ReadAllBytes(ReturnFullPath(fileDto));
        }

        //Ok
        public void Delete(FileDTO file)
        {
            _data.Files.Delete(file.Id);
            File.Delete(ReturnFullPath(file));
            _data.Save();
        }

        //Ok
        public void Dispose()
        {
            _data.Dispose();
        }

        //Ok
        public HashSet<FileDTO> GetAll()
        {
            return Mapper.Map<HashSet<FileDTO>>(_data.Files.GetAll());
        }

        //Ok
        public List<FileDTO> GetAllByUserId(string userid)
        {
            return GetAll().Where(f => f.Folder.UserId.Equals(userid)).ToList();
        }

        //Ok
        public void EditFile(int id, FileDTO fileDto)
        {
            var newFile = _data.Files.Get(id);
            string oldPath = ReturnFullPath(Mapper.Map<FileDTO>(newFile));
            newFile.Name = fileDto.Name;
            string newPath = ReturnFullPath(Mapper.Map<FileDTO>(newFile));
            File.Move(oldPath, newPath);
            newFile.AccessLevel = fileDto.AccessLevel;
            newFile.IsBlocked = fileDto.IsBlocked;
            _data.Files.Update(newFile);
            _data.Save();
        }

        //Ok
        public bool IsFileExists(FileDTO file)
        {
            return File.Exists(ReturnFullPath(file));
        }
    }
}