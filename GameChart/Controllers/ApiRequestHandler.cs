using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using GameChart.Models.Games;

namespace GameChart.Controllers
{
    public class ApiRequestHandler
    {
        public string ApiCall(string apirequest)
        {
            WebClient webclient = new WebClient();
            webclient.Headers.Add("user-key", GetKey());
            webclient.Headers.Add("Accept", "application/json");
            try
            {
                return webclient.DownloadString("https://api-endpoint.igdb.com/" + apirequest);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private string GetKey()
        {
            var key = WebConfigurationManager.AppSettings["APIKey"];
            return key;
        }

        internal Game GetGameById(long id)
        {
            WebClient webclient = new WebClient();
            webclient.Headers.Add("user-key", GetKey());
            webclient.Headers.Add("Accept", "application/json");
            try
            {
                var jsonResult = webclient.DownloadString("https://api-endpoint.igdb.com/" + "games/" + id);
                var game = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Game>>(jsonResult)[0] as Game;
                return game;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal List<Game> SearchGames(string name)
        {
            WebClient webclient = new WebClient();
            webclient.Headers.Add("user-key", GetKey());
            webclient.Headers.Add("Accept", "application/json");
            try
            {
                var jsonResult = webclient.DownloadString("https://api-endpoint.igdb.com/" + "games/?search=" + name);
                var game = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Game>>(jsonResult);
                return game;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}