using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameChart.Controllers;
using Newtonsoft.Json;

namespace GameChart.Models.Games
{
    public class GameSearch
    {
        public List<Id> Id { get; set; }

        public async System.Threading.Tasks.Task<List<Game>> ToGameListAsync(ApiRequestHandler handler)
        {
            List<Game> games = new List<Game>();
            foreach (var id in Id)
            {
                games.Add(await handler.GetGameByIdAsync(id.Idv));
            }
            return games;
        }
    }
}