﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameChart.Models.Games
{
    public class GamesByGenre
    {
        public Genres Name { get; set; }
        public List<GameShort> Games { get; set; } = new List<GameShort>();

        public GamesByGenre(GameShort game, Genres name)
        {
            Games.Add(game);
            Name = name;
        }

        public GamesByGenre()
        {

        }
    }
}