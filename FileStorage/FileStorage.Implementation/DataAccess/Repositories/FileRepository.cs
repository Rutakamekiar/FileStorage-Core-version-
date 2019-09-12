using System;
using System.Linq;
using FileStorage.Implementation.DataAccess.Entities;
using FileStorage.Implementation.DataAccess.Entity_Framework;
using FileStorage.Implementation.DataAccess.RepositoryInterfaces;
using FileStorage.Implementation.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.Implementation.DataAccess.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly StorageContext _context;

        public FileRepository(StorageContext context)
        {
            _context = context ?? throw new ArgumentNullException("Context must be not null!");
        }

        public void Create(FileEntity item)
        {
            _context.Set<FileEntity>().Add(item ?? throw new ArgumentNullException("File must be not null!"));
        }

        public void Delete(Guid id)
        {
            var file = Get(id);
            _context.Set<FileEntity>().Remove(file);
        }

        public FileEntity Get(Guid id)
        {
            return _context.Set<FileEntity>().Include(f => f.FolderEntity).FirstOrDefault(f => f.Id == id)
                   ?? throw new FileNotFoundException($"File with id = {id} was not found");
        }

        public IQueryable<FileEntity> GetAll()
        {
            return _context.Set<FileEntity>().Include(f => f.FolderEntity);
        }

        public void Update(FileEntity fileEntity)
        {
            _context.Entry(fileEntity).State = EntityState.Modified;
        }
    }
}