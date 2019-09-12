using System;
using System.Linq;

namespace FileStorage.Implementation.DataAccess.RepositoryInterfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T Get(Guid id);
        void Create(T item);
        void Delete(Guid id);
        void Update(T item);
    }
}