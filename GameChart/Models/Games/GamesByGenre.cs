using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameChart.Models.Games
{
    public class GamesByGenre
    {
        public string Name { get; set; }
        public List<GameShort> Games { get; set; } = new List<GameShort>();

        public GamesByGenre(GameShort game, string name)
        {
            Games.Add(game);
            Name = name;
        }

        public GamesByGenre()
        {

        }
    }
}