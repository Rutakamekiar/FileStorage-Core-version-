using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.WebApi.Models.Requests
{
    public class UploadFileRequest
    {
        public Guid FolderId { get; set; }
        public bool AccessLevel { get; set; }
    }
}
