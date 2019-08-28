using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Cookidea.Models
{
    [Table("Album")]
    public class Album
    {

        [JsonProperty("tracklist")]
        public string TractList { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id"), PrimaryKey, ForeignKey(typeof(Album))]
        public string Id { get; set; }

        [JsonProperty("cover_medium")]
        public string Cover { get; set; }


        public Album()
        {

        }

    }



}
