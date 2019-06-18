namespace BLL.DTO
{
    public class UserDTO : IEntityDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public RoleDTO Role { get; set; }
        public long MemorySize { get; set; }
    }
}