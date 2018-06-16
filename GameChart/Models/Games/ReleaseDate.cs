using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameChart.Models.Games
{
    public partial class ReleaseDate
    {
        [JsonProperty("category")]
        public long Category { get; set; }

        [JsonProperty("platform")]
        public long Platform { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("region")]
        public long Region { get; set; }

        [JsonProperty("human")]
        public string Human { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }

        [JsonProperty("m")]
        public long M { get; set; }
    }
}