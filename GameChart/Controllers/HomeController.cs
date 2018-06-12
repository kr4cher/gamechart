using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}