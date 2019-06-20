using Newtonsoft.Json;
using System.Linq;

namespace WebApi.Models
{
    public class FileView
    {
        private string fileType;

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

        public string FileType
        {
            get
            {
                return Name.Split(".").Last();
            }
        }
    }
}