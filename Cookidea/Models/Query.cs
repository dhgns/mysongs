using System.Globalization;
using Cookidea.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QuickType
{
    public class Query
    {
        [JsonProperty("data")]
        public Song[] Songs { get; set; }

        public static Query FromJson(string json) => JsonConvert.DeserializeObject<Query>(json, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
