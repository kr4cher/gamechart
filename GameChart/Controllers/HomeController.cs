using GameChart.Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Web.Configuration;
using System.Web.Mvc;

namespace GameChart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string medalsXML = System.IO.File.ReadAllText(Server.MapPath("Views/Shared/medals.xml"));
            ViewBag.Medals = medalsXML;
            return View();
        }

        [HttpGet]
        public JsonResult APIAnswer(string[] call)
        {
            var data = ApiCall(call[0]);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private string ApiCall(string apirequest)
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
    }
}