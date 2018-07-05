using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GameChart.Models.Games  
{
    public partial class Game :GameShort
    {
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("collection")]
        public long Collection { get; set; }

        [JsonProperty("franchise")]
        public long Franchise { get; set; }

        [JsonProperty("franchises")]
        public long[] Franchises { get; set; }

        [JsonProperty("games")]
        public long[] GameGames { get; set; }

        [JsonProperty("tags")]
        public long[] Tags { get; set; }

        [JsonProperty("developers")]
        public long[] Developers { get; set; }

        [JsonProperty("publishers")]
        public long[] Publishers { get; set; }

        [JsonProperty("game_engines")]
        public long[] GameEngines { get; set; }

        [JsonProperty("category")]
        public long Category { get; set; }

        [JsonProperty("time_to_beat")]
        public TimeToBeat TimeToBeat { get; set; }

        [JsonProperty("player_perspectives")]
        public long[] PlayerPerspectives { get; set; }

        [JsonProperty("game_modes")]
        public long[] GameModes { get; set; }

        [JsonProperty("keywords")]
        public long[] Keywords { get; set; }

        [JsonProperty("themes")]
        public long[] Themes { get; set; }
        
        [JsonProperty("platforms")]
        public long[] Platforms { get; set; }

        [JsonProperty("release_dates")]
        public ReleaseDate[] ReleaseDates { get; set; }

        [JsonProperty("alternative_names")]
        public AlternativeName[] AlternativeNames { get; set; }
    }
}