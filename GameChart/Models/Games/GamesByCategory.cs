using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameChart.Models.Games
{
    public class GamesByCategory
    {
        public Category Name { get; set; }
        public List<GameShort> Games { get; set; } = new List<GameShort>();

        public GamesByCategory(GameShort category, Category name)
        {
            Games.Add(category);
            Name = name;
        }

        public GamesByCategory()
        {

        }
    }
}