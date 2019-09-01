using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MySongs.Models
{
    [Table("Song")]
    public class Song
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
        [JsonProperty("album"), OneToMany]
        public Album Album { get; set; }

        [JsonProperty("artist"), OneToMany]
        public Artist Artist { get; set; }


        public Song()
        {

        }

        public SongDAO ToDAO()
        {
            return new SongDAO(link: Link, title:Title, type: Type, id: SongId, ranking: SocialRank, duration: Duration, isFavorite: isFavorite, album:Album.Id, artist:Artist.Id );
        }

        public Song(string link, string title, string type, string id, double ranking, string duration, bool isFavorite, string artist, string album)
        {
            var Album = new Album();
            var Artist = new Artist();

            this.Link = link; this.Title = title; this.Type = type; this.SongId = id; this.SocialRank = ranking;
            this.Duration = duration; this.isFavorite = isFavorite; this.Album = Album; this.Artist = this.Artist;
        }
    }
}
