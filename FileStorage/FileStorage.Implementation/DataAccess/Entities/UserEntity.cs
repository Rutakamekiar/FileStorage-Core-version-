using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FileStorage.Implementation.DataAccess.Entities
{
    public class UserEntity : IdentityUser
    {
        [Required]
        public long MemorySize { get; set; } = 100000000;
    }
}