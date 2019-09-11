namespace BLL.DTO
{
    public sealed class FileDto : IEntityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AccessLevel { get; set; }
        public bool IsBlocked { get; set; }
        public int FolderId { get; set; }
        public FolderDto Folder { get; set; }
        public byte[] FileBytes { get; set; }
    }
}