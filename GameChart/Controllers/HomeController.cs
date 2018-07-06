using GameChart.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Web.Mvc;
using System.IO;
using System.Text;

namespace GameChart.Controllers
{
    public class HomeController : Controller
    {
        static public ApiRequestHandler ApiRequest = new ApiRequestHandler();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<string> APIAnswerAsync(string call)
        {
            try
            {
                var data = ApiRequest.ApiCallAsync(call);
                var game = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameShort>>(await data);
                return ApiRequest.ToXML(game);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<string> GamesByPopularityAsync()
        {
            try
            {
                var data = ApiRequest.ApiCallAsync("/games/?fields=name,popularity,genres&order=popularity:desc&limit=50");
                var games = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameShort>>(await data);
                long max = 0;
                foreach (var game in games)
                {
                    if (game.Genres != null)
                    {
                        var m = game.Genres.Max();
                        if (m > max)
                        {
                            max = m;
                        }
                    }                    
                }
                GamesByGenre[] gamesbg = new GamesByGenre[max+1];
                foreach (var game in games)
                {
                    if (game.Genres != null)
                    {
                        foreach (var ger in game.Genres)
                        {
                            if (gamesbg[ger] == null)
                            {
                                gamesbg[ger] = new GamesByGenre(game, await ApiRequest.GetGenreName(ger));
                            }
                            else
                            {
                                gamesbg[ger].Games.Add(game);
                            }
                        }
                    }
                }
                List<GamesByGenre> returnGames = new List<GamesByGenre>();
                foreach (var item in gamesbg)
                {
                    if (item != null)
                    {
                        returnGames.Add(item);
                    }
                }
                return ApiRequest.ToXML(returnGames);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<string> SearchGamesAsync(string call)
        {
            try
            {
                List<Game> games = await ApiRequest.SearchGamesAsync(call);
                List<GameShort> gamesShort = new List<GameShort>(games.Count);
                foreach (var game in games)
                {
                    gamesShort.Add(game as GameShort);
                }
                var xml = ApiRequest.ToXML(games);
                return xml;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<string> GameByIdAsync(string call)
        {
            try
            {
                if (Int32.TryParse(call, out int id))
                {
                    Game data = await ApiRequest.GetGameByIdAsync(id);
                    string xml = ApiRequest.ToXML(data);
                    return xml;
                }
                throw new ApplicationException("id was not a string");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}