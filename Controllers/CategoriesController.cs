using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using NewlyReadCore.SQLite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewlyReadCore{
    public class CategoriesController : Controller{
        public ActionResult Index(string category){
            ViewBag.Category = category;
            ViewBag.List = ReadAPI.GetArticlesByCategory(category, 10);
            return View("Index");
        }

        public ActionResult Article(string url){
            var db = new MyDBContext();
            var matching_articles  = db.Articles.Where(i => i.url == url);
            var first_match = new Article();
            if(matching_articles.Count() > 0){
                first_match = matching_articles.First();
            }
            ViewBag.Nodes = ReadAPI.ExtractHtmlFromURL(url);
            ViewBag.Original = url;
            return View();
        }
    }
}