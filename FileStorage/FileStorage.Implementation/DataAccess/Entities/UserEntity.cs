using Microsoft.AspNetCore.Identity;

namespace FileStorage.Implementation.DataAccess.Entities
{
    public class UserEntity : IdentityUser
    {
        public long MemorySize { get; set; }
    }
}