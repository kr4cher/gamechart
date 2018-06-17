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
            var data = ApiRequest.ApiCall(call);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}