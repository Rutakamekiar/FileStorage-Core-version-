using System;

namespace FileStorage.Contracts.Requests
{
    public class UploadFileRequest
    {
        public Guid FolderId { get; set; }
        public bool AccessLevel { get; set; }
    }
}
