using System.Collections.Generic;

namespace BLL.DTO
{
    public class FolderDto : IEntityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Path { get; set; }
        public ICollection<FileDto> Files { get; set; }
        public ICollection<FolderDto> Folders { get; set; }
        public int? ParentFolderId { get; set; }
        public FolderDto ParentFolder { get; set; }
    }
}