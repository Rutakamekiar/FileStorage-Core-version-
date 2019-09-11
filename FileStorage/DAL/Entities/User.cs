using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }

        [Required] public int RoleId { get; set; }

        public virtual Role Role { get; set; }

        [Required] public long MemorySize { get; set; } = 100000000;
    }
}