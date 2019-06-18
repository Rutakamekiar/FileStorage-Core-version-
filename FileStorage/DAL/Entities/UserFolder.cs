using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class UserFolder : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Path { get; set; }

        public ICollection<UserFile> Files { get; set; }
        public ICollection<UserFolder> Folders { get; set; }
        public int? ParentFolderId { get; set; }
        public virtual UserFolder ParentFolder { get; set; }

        public UserFolder()
        {
            Files = new List<UserFile>();
            Folders = new List<UserFolder>();
        }
    }
}