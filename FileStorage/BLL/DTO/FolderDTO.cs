using System.Collections.Generic;

namespace BLL.DTO
{
    public class FolderDTO : IEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Path { get; set; }
        public ICollection<FileDTO> Files { get; set; }
        public ICollection<FolderDTO> Folders { get; set; }
        public int? ParentFolderId { get; set; }
        public FolderDTO ParentFolder { get; set; }
    }
}
