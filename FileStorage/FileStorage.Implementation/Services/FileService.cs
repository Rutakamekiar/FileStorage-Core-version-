using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateAsync(MyFile item)
        {
            await _data.Files.CreateAsync(_mapper.Map<FileEntity>(item));
            File.WriteAllBytes(await ReturnFullPathAsync(item), item.FileBytes);

            await _data.SaveAsync();
        }

        public async Task<string> ReturnFullPathAsync(MyFile file)
        {
            var folder = await _data.Folders.GetAsync(file.FolderId);
            return $@"{RootFilePath}\{folder.Path}\{folder.Name}\{file.Name}";
        }

        public MyFile Get(Guid id)
        {
            return _mapper.Map<MyFile>(_data.Files.GetAsync(id));
        }

        public async Task<byte[]> GetFileBytesAsync(MyFile fileDto)
        {
            return File.ReadAllBytes(await ReturnFullPathAsync(fileDto));
        }

        public async Task DeleteAsync(MyFile file)
        {
            await _data.Files.DeleteAsync(file.Id);
            File.Delete(await ReturnFullPathAsync(file));
            await _data.SaveAsync();
        }

        public void Dispose()
        {
            _data.Dispose();
        }

        public async Task<HashSet<MyFile>> GetAllAsync()
        {
            return _mapper.Map<HashSet<MyFile>>(await _data.Files.GetAllAsync());
        }

        public async Task EditFileAsync(Guid id, MyFile fileDto)
        {
            var newFile = await _data.Files.GetAsync(id);
            var oldPath = await ReturnFullPathAsync(_mapper.Map<MyFile>(newFile));
            newFile.Name = fileDto.Name;
            var newPath = await ReturnFullPathAsync(_mapper.Map<MyFile>(newFile));
            File.Move(oldPath, newPath);
            newFile.AccessLevel = fileDto.AccessLevel;
            newFile.IsBlocked = fileDto.IsBlocked;
            _data.Files.Update(newFile);
            await _data.SaveAsync();
        }

        public async Task<bool> IsFileExistsAsync(MyFile file)
        {
            return File.Exists(await ReturnFullPathAsync(file));
        }

        public async Task<List<MyFile>> GetAllByUserIdAsync(string userid)
        {
            return (await GetAllAsync()).Where(f => f.Folder.UserId.Equals(userid)).ToList();
        }
    }
}