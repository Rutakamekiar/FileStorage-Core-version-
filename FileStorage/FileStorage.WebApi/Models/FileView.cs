using System;
using Newtonsoft.Json;

namespace FileStorage.WebApi.Models
{
    public class FileView
    {
        [JsonProperty(PropertyName = "id")] public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")] public string Name { get; set; }

        [JsonProperty(PropertyName = "accessLevel")]
        public bool AccessLevel { get; set; }

        [JsonProperty(PropertyName = "isBlocked")]
        public bool IsBlocked { get; set; }

        [JsonProperty(PropertyName = "folderId")]
        public Guid FolderId { get; set; }
    }
}