using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameChart.Models.Games
{
    public class GameShort :IComparable<GameShort>
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("first_release_date")]
        public long FirstReleaseDate { get; set; }
        
        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("genres")]
        public long[] Genres { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("release_dates")]
        public ReleaseDate[] ReleaseDates { get; set; }

        public int CompareTo(GameShort game)
        {
            if (FirstReleaseDate == 0 && game.FirstReleaseDate == 0)
            {
                return 0;
            }
            else if (game.FirstReleaseDate == 0)
            {
                return 1;
            }
            else if (FirstReleaseDate == 0)
            {
                return -1;
            }
            else if (FirstReleaseDate < game.FirstReleaseDate)
            {
                return 1;
            }
            else if(FirstReleaseDate > game.FirstReleaseDate)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}