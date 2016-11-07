using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using NewlyReadCore.SQLite;
using System.Linq;

namespace NewlyReadCore.Controllers {
    public class HomeController : Controller {
        public IActionResult Index(){
            var db = new MyDBContext();
            ViewBag.DB = ReadAPI.SummarizeLatestArticles(1);
            return View();
        }

        public IActionResult About(){
            return View("About");
        }
    }
}