using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace VisitorCounter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult GetMessage()
        {
            HttpClient client = new HttpClient();
            String response = client.GetStringAsync("https://az3tnb2cq2.execute-api.us-east-1.amazonaws.com/Prod/api/values").Result;
            ViewData["LuckyNumbers"] = response;

            var views = (int)(Session["ViewCount"] ?? 0) + 1;
            Session["ViewCount"] = views;
            ViewData["Message"] = views;
            String message = "Your connection token for today is:<b> " + response.Replace(" ","") + " </b>and you have retrieved a token <b>" + views + "</b> time(s) during this session";
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
         

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}