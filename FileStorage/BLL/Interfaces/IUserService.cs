using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IUserService : IService<UserDto>
    {
        UserDto GetByEmailAndPassword(string email, string password);

        UserDto GetByEmail(string email);

        void ChangeUserMemorySize(string email, long memorySize);
    }
}