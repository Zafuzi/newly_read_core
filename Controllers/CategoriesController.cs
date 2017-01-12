using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewlyReadCore.SQLite;
using Newtonsoft.Json;

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
            bool isNode = false;
            if(matching_articles.Count() > 0){
                first_match = matching_articles.First();
                if(first_match.jsonstring != null && first_match.jsonstring.Length > 0){
                    ViewBag.Article = first_match;
                }else{
                    ViewBag.Nodes = ReadAPI.ExtractHtmlFromURL(url);
                    isNode = true;
                    first_match.jsonstring = JsonConvert.SerializeObject(ViewBag.Nodes["article"]);
                }
            }
            ViewBag.isNode = isNode;
            ViewBag.Original = url;
            return View();
        }
    }
}