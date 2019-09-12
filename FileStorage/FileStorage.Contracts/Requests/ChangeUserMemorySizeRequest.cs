using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileStorage.WebApi.Models.Requests
{
    public class ChangeUserMemorySizeRequest
    {
        public string UserId { get; set; }
        public long MemorySize { get; set; }
    }
}
