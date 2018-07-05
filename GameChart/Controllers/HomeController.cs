using GameChart.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace GameChart.Controllers
{
    public class HomeController : Controller
    {
        readonly ApiRequestHandler ApiRequest = new ApiRequestHandler();

        public ActionResult Index()
        {
            string medalsXML = System.IO.File.ReadAllText(Server.MapPath("Views/Shared/medals.xml"));
            ViewBag.Medals = medalsXML;
            return View();
        }

        [HttpGet]
        public string APIAnswer(string apiQuery)
        {
            try
            {
                var data = ApiRequest.ApiCall("/games/?fields=name,popularity&order=popularity:desc&limit=20");
                var game = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameShort>>(data);
                return ToXML<Game>(game);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string ToXML(T game)
        {
            XmlSerializer ser = new XmlSerializer(game.GetType());
            string t = "";
            using (MemoryStream st = new MemoryStream())
            {
                ser.Serialize(st, game);
                var buffer = st.ToArray();
                t = System.Text.Encoding.Default.GetString(buffer);

            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public string SearchGames(string gameName)
        {
            try
            {
                List<Game> data = ApiRequest.SearchGames(gameName);
                List<Game> games = new List<Game>();
                foreach (var game in data)
                {
                    games.Add(ApiRequest.GetGameById(game.Id));
                }
                XmlSerializer ser = new XmlSerializer(typeof(List<Game>), "GameChart");
                string xml;
                using (var stream = new StringWriter())
                {
                    ser.Serialize(stream, data);
                    xml = stream.ToString();
                }
                return xml;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        public string GameById(string idString)
        {
            try
            {
                if (Int32.TryParse(idString, out int id))
                {
                    Game data = ApiRequest.GetGameById(id);
                    XmlSerializer ser = new XmlSerializer(typeof(Game), "GameChart");
                    string xml;
                    using (var stream = new StringWriter())
                    {
                        ser.Serialize(stream, data);
                        xml = stream.ToString();
                    }
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