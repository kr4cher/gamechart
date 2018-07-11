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
        Dictionary<long, Genres> GenreCache { get; set; } = new Dictionary<long, Genres>();
        Dictionary<long, Game> GameCache { get; set; } = new Dictionary<long, Game>();
        Dictionary<long, Category> CategoryCache { get; set; } = new Dictionary<long, Category>();


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

        internal async System.Threading.Tasks.Task<Genres> GetGenre(long ger)
        {
            if (GenreCache.TryGetValue(ger, out Genres genre))
            {
                return genre;
            }
            var jsonResult = await webclient.DownloadStringTaskAsync("https://api-endpoint.igdb.com/" + "genres/" + ger);
            var ge = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Genres>>(jsonResult)[0];
            GenreCache.Add(ger, ge);
            return ge;
        }

        internal async System.Threading.Tasks.Task<Category> GetCategoryName(long cat)
        {
            if (CategoryCache.TryGetValue(cat, out Category category))
            {
                return category;
            }
            var jsonResult = await webclient.DownloadStringTaskAsync("https://api-endpoint.igdb.com/" + "category/" + cat);
            var ca = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(jsonResult)[0];
            CategoryCache.Add(cat, ca);
            return ca;
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