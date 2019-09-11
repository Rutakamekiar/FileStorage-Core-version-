using System;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IService<T> : IDisposable where T : IEntityDto
    {
        HashSet<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Delete(T item);
    }
}