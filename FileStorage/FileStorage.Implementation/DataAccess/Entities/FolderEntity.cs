using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileStorage.Implementation.DataAccess.Entities
{
    public class FolderEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Path { get; set; }

        public ICollection<FileEntity> Files { get; set; }
        public ICollection<FolderEntity> Folders { get; set; }
        public Guid? ParentFolderId { get; set; }
        public virtual FolderEntity ParentFolderEntity { get; set; }
    }
}