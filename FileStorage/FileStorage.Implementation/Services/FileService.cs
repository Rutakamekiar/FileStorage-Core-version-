using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using FileStorage.Contracts;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.Interfaces;

namespace FileStorage.Implementation.Services
{
    public class FileService : IFileService
    {
        private const string RootFilePath =
            @"C:\Users\Vlad\Desktop\Core\_FileStorage\FileStorageFront\src\assets\Content";

        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;

        public FileService(IUnitOfWork data,
                           IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        //Ok
        public void Create(MyFile item)
        {
            _data.Files.Create(_mapper.Map<FileEntity>(item));
            File.WriteAllBytes(ReturnFullPath(item), item.FileBytes);

            _data.Save();
        }

        //Ok
        public string ReturnFullPath(MyFile file)
        {
            var folder = _data.Folders.Get(file.FolderId);
            return $@"{RootFilePath}\{folder.Path}\{folder.Name}\{file.Name}";
        }

        //Ok
        public MyFile Get(Guid id)
        {
            return _mapper.Map<MyFile>(_data.Files.Get(id));
        }

        //Ok
        public byte[] GetFileBytes(MyFile fileDto)
        {
            return File.ReadAllBytes(ReturnFullPath(fileDto));
        }

        //Ok
        public void Delete(MyFile file)
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
        public HashSet<MyFile> GetAll()
        {
            return _mapper.Map<HashSet<MyFile>>(_data.Files.GetAll());
        }

        //Ok
        public void EditFile(Guid id, MyFile fileDto)
        {
            var newFile = _data.Files.Get(id);
            var oldPath = ReturnFullPath(_mapper.Map<MyFile>(newFile));
            newFile.Name = fileDto.Name;
            var newPath = ReturnFullPath(_mapper.Map<MyFile>(newFile));
            File.Move(oldPath, newPath);
            newFile.AccessLevel = fileDto.AccessLevel;
            newFile.IsBlocked = fileDto.IsBlocked;
            _data.Files.Update(newFile);
            _data.Save();
        }

        //Ok
        public bool IsFileExists(MyFile file)
        {
            return File.Exists(ReturnFullPath(file));
        }

        //Ok
        public List<MyFile> GetAllByUserId(string userid)
        {
            return GetAll().Where(f => f.Folder.UserId.Equals(userid)).ToList();
        }
    }
}