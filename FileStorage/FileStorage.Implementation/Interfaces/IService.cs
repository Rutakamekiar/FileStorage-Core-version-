using System;
using System.Collections.Generic;

namespace FileStorage.Implementation.Interfaces
{
    public interface IService<T> : IDisposable
    {
        HashSet<T> GetAll();
        T Get(Guid id);
        void Create(T item);
        void Delete(T item);
    }
}