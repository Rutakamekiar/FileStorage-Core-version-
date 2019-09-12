using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FileStorage.WebApi.Models
{
    public class FolderView
    {
        [JsonProperty(PropertyName = "id")] public Guid Id { get; set; }

        [JsonProperty(PropertyName = "name")] public string Name { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "path")] public string Path { get; set; }

        [JsonProperty(PropertyName = "files")] public List<FileView> Files { get; set; }

        [JsonProperty(PropertyName = "folders")]
        public List<FolderView> Folders { get; set; }

        [JsonProperty(PropertyName = "parentFolderId")]
        public Guid? ParentFolderId { get; set; }
    }
}