using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameChart.Models.Games
{
    public partial class TimeToBeat
    {
        [JsonProperty("hastly")]
        public long Hastly { get; set; }
    }
}