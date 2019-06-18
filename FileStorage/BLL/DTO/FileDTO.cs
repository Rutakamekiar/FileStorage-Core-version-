namespace BLL.DTO
{
    public sealed class FileDTO : IEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AccessLevel { get; set; }
        public bool IsBlocked { get; set; }
        public int FolderId { get; set; }
        public FolderDTO Folder { get; set; }
        public byte[] FileBytes { get; set; }
    }
}