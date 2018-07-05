using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameChart.Models.Games
{
    public class GameShort
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("first_release_date")]
        public long FirstReleaseDate { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("total_rating")]
        public double TotalRating { get; set; }

        [JsonProperty("total_rating_count")]
        public long TotalRatingCount { get; set; }

        [JsonProperty("rating_count")]
        public long RatingCount { get; set; }

        [JsonProperty("genres")]
        public long[] Genres { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }
    }
}