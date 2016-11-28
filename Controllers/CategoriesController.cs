using System;
using System.Collections.Generic;
using System.Linq;
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
            Dictionary<string, List<string>> json = ReadAPI.ExtractHtmlFromURL(url);
            // if(first_match.jsonstring != null){
            //     json = first_match.jsonstring;
            // }else{
            //     json = ReadAPI.ExtractHtmlFromURL(url);
                //first_match.jsonstring = JsonConvert.DeserializeObject(json).ToString();
            //     first_match.jsonstring = json;
            //     db.SaveChanges();
            // }
            ViewBag.JSON = json;
            return View();
        }
    }
}