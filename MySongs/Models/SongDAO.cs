using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLitePCL;

namespace MySongs.Models
{
    [Table("SongDAO")]
    public class SongDAO
    {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id"), PrimaryKey]
        public string SongId { get; set; }

        [JsonProperty("rank")]
        public double SocialRank { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        public bool isFavorite { get; set; }

        [JsonProperty("album")]
        public string Album { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        public SongDAO()
        {

        }

        public SongDAO(string link, string title, string type, string id, double ranking, string duration, bool isFavorite, string album, string artist)
        {
            this.Link = link;
            this.Title = title;
            this.Type = type;
            this.SocialRank = ranking;
            this.Duration = duration;
            this.isFavorite = isFavorite;
            this.Album = album;
            this.Artist = artist;
            this.SongId = id;
        }

        internal Song fromDAO()
        {
            return new Song(link:Link, title:Title, type:Type, id:SongId, ranking:SocialRank, duration:Duration, isFavorite:isFavorite, album:Album, artist:Artist);
        }
    }
}
