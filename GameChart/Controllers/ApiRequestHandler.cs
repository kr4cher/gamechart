using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;

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
    }
}