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
    public class FileService : IFileService
    {
        private const string RootFilePath =
            @"C:\Users\Vlad\Desktop\Core\_FileStorage\FileStorageFront\src\assets\Content";

        private readonly IUnitOfWork _data;

        public FileService(IUnitOfWork data)
        {
            _data = data;
        }

        //Ok
        public void Create(FileDto item)
        {
            _data.Files.Create(Mapper.Map<UserFile>(item));
            File.WriteAllBytes(ReturnFullPath(item), item.FileBytes);

            _data.Save();
        }

        //Ok
        public string ReturnFullPath(FileDto file)
        {
            var folder = _data.Folders.Get(file.FolderId);
            return $@"{RootFilePath}\{folder.Path}\{folder.Name}\{file.Name}";
        }

        //Ok
        public FileDto Get(int id)
        {
            return Mapper.Map<FileDto>(_data.Files.Get(id));
        }

        //Ok
        public byte[] GetFileBytes(FileDto fileDto)
        {
            return File.ReadAllBytes(ReturnFullPath(fileDto));
        }

        //Ok
        public void Delete(FileDto file)
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
        public HashSet<FileDto> GetAll()
        {
            return Mapper.Map<HashSet<FileDto>>(_data.Files.GetAll());
        }

        //Ok
        public List<FileDto> GetAllByUserId(string userid)
        {
            return GetAll().Where(f => f.Folder.UserId.Equals(userid)).ToList();
        }

        //Ok
        public void EditFile(int id, FileDto fileDto)
        {
            var newFile = _data.Files.Get(id);
            var oldPath = ReturnFullPath(Mapper.Map<FileDto>(newFile));
            newFile.Name = fileDto.Name;
            var newPath = ReturnFullPath(Mapper.Map<FileDto>(newFile));
            File.Move(oldPath, newPath);
            newFile.AccessLevel = fileDto.AccessLevel;
            newFile.IsBlocked = fileDto.IsBlocked;
            _data.Files.Update(newFile);
            _data.Save();
        }

        //Ok
        public bool IsFileExists(FileDto file)
        {
            return File.Exists(ReturnFullPath(file));
        }
    }
}