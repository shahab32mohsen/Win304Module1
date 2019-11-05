using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;

namespace VisitorCounter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return Redirect("/chat");
            }
            var views = (int)(Session["ViewCount"] ?? 0) + 1;
            Session["ViewCount"] = views;
            ViewData["Message"] = views;
            WebRequest request = WebRequest.Create("https://az3tnb2cq2.execute-api.us-east-1.amazonaws.com/Prod/api/values");
            WebResponse response = request.GetResponse();
            //Stream dataStream = response.GetResponseStream();
            using (Stream dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                ViewData["LuckyNumbers"] = responseFromServer;
            }
            response.Close();
            
            return View();
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