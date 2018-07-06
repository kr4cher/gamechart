using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Xml.Serialization;
using GameChart.Models.Games;

namespace GameChart.Controllers
{
    public class ApiRequestHandler
    {
        WebClient webclient { get; set; } = new WebClient();
        Dictionary<long, string> GenreCache { get; set; } = new Dictionary<long, string>();
        Dictionary<long, Game> GameCache { get; set; } = new Dictionary<long, Game>();


        public ApiRequestHandler()
        {
            webclient.Headers.Add("user-key", GetKey());
            webclient.Headers.Add("Accept", "application/json");
        }

        public async System.Threading.Tasks.Task<string> ApiCallAsync(string apirequest)
        {
            try
            {
                Uri uri = new Uri("https://api-endpoint.igdb.com/" + apirequest);
                return await webclient.DownloadStringTaskAsync(uri);
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

        internal async System.Threading.Tasks.Task<Game> GetGameByIdAsync(long id)
        {
            try
            {
                if (GameCache.TryGetValue(id, out Game gameInCache))
                {
                    return gameInCache;
                }
                Uri uri = new Uri("https://api-endpoint.igdb.com/" + "games/" + id);
                var jsonResult = webclient.DownloadStringTaskAsync(uri);
                var game = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Game>>(await jsonResult)[0] as Game;
                GameCache.Add(id, game);
                return game;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal async System.Threading.Tasks.Task<List<Game>> SearchGamesAsync(string name)
        {
            try
            {
                var jsonResult = webclient.DownloadStringTaskAsync("https://api-endpoint.igdb.com/" + "games/?search=" + name);
                var ids = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Id>>(await jsonResult);
                var gameSearch = new GameSearch();
                gameSearch.Id = ids;
                var games = gameSearch.ToGameListAsync(this);
                return await games;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal async System.Threading.Tasks.Task<string> GetGenreName(long ger)
        {
            if (GenreCache.TryGetValue(ger, out string name))
            {
                return name;
            }
            var jsonResult = await webclient.DownloadStringTaskAsync("https://api-endpoint.igdb.com/" + "genres/" + ger);
            var genre = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Genres>>(jsonResult)[0];
            GenreCache.Add(ger, genre.Name);
            return genre.Name;
        }

        public string ToXML<T>(T game)
        {
            XmlSerializer ser = new XmlSerializer(game.GetType());
            string xml = "";
            using (MemoryStream st = new MemoryStream())
            {
                ser.Serialize(st, game);
                var buffer = st.ToArray();
                xml = Encoding.Default.GetString(buffer);
            }
            return xml;
        }
    }
}