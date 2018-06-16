using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameChart.Models.Games
{

    public partial class AlternativeName
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}