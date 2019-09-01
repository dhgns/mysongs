using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
namespace MySongs.Models
{
    [Table("Artist")]
    public class Artist
    {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id"), PrimaryKey, ForeignKey(typeof(Album))]
        public string Id { get; set; }

        [JsonProperty("picture_medium")]
        public string Picture { get; set; }

        public Artist() { }
    }

}



