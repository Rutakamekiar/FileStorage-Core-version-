using System.Linq;
using DAL.Entities;

namespace DAL.Interfaces.RepositoryInterfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Delete(int id);
        void Update(T item);
    }
}