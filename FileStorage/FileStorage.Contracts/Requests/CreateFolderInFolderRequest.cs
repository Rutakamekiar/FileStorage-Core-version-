using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.WebApi.Models.Requests
{
    public class CreateFolderInFolderRequest
    {
        public Guid ParentId { get; set; }
        public string Name { get; set; }
    }
}
