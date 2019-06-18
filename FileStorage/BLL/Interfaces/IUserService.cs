using BLL.DTO;
using BLL.Interfaces;

namespace BLL.Services
{
    public interface IUserService : IService<UserDTO>
    {
        UserDTO GetByEmailAndPassword(string email, string password);

        UserDTO GetByEmail(string email);

        void ChangeUserMemorySize(string email, long memorySize);
    }
}