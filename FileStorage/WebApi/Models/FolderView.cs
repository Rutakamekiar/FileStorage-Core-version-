using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class FolderView
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "files")]
        public List<FileView> Files { get; set; }

        [JsonProperty(PropertyName = "folders")]
        public List<FolderView> Folders { get; set; }

        [JsonProperty(PropertyName = "parentFolderId")]
        public int? ParentFolderId { get; set; }
    }
}