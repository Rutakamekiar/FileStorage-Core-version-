using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class UserFile : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool AccessLevel { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        [Required]
        public int FolderId { get; set; }

        public UserFolder Folder { get; set; }
    }
}