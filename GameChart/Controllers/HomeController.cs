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
        public JsonResult APIAnswer(string call)
        {
            var data = ApiRequest.ApiCall("/games/?fields=name,popularity&order=popularity:desc&limit=20");
            var game = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameShort>>(data);
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
    }
}