using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class FileView
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "accessLevel")]
        public bool AccessLevel { get; set; }

        [JsonProperty(PropertyName = "isBlocked")]
        public bool IsBlocked { get; set; }

        [JsonProperty(PropertyName = "folderId")]
        public int FolderId { get; set; }
    }
}