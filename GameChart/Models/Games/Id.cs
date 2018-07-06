using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace GameChart.Models.Games
{
    public class Id
    {
        [JsonProperty("id")]
        public long Idv { get; set; }
    }
}