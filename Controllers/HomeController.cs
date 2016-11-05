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
            Dictionary<string, Article[]> articles = new Dictionary<string, Article[]>{
                ["business"] = db.Articles.Take(10).Where(i => i.category == "technology").OrderBy(p => p.timestamp).ToArray()
            };
            ViewBag.DB = db.Articles.Take(10).Where(i => i.category == "technology").OrderBy(p => p.timestamp).ToArray();
            return View();
        }

        public IActionResult About(){
            return View("About");
        }
    }
}