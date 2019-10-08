using System;

namespace FileStorage.Contracts.Requests
{
    public class CreateFolderInFolderRequest
    {
        public Guid ParentId { get; set; }
        public string Name { get; set; }
    }
}
