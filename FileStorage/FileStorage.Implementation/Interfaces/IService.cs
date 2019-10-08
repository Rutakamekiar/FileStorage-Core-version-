using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileStorage.Implementation.Interfaces
{
    public interface IService<T> : IDisposable
    {
        Task<HashSet<T>> GetAllAsync();
        T Get(Guid id);
        Task CreateAsync(T item);
        Task DeleteAsync(T item);
    }
}