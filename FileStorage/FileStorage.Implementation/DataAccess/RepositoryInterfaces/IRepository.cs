using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileStorage.Implementation.DataAccess.Entities;

namespace FileStorage.Implementation.DataAccess.RepositoryInterfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task CreateAsync(T item);
        Task DeleteAsync(Guid id);
        void Update(T item);
    }
}