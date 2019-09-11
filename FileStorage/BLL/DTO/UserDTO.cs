namespace BLL.DTO
{
    public class UserDto : IEntityDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public RoleDto Role { get; set; }
        public long MemorySize { get; set; }
    }
}