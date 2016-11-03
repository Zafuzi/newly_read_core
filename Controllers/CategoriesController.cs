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
            Console.WriteLine("\n\n\n" + category + " \n\n\n");
            ViewBag.Category = category;
            Article[] cat_articles = ReadAPI.GetArticlesByCategory(category);
            ViewBag.CatArticles = cat_articles;
            return View("Index");
        }

        public ActionResult Article(string url){
            var db = new MyDBContext();
            var matching_articles  = db.Articles.Where(i => i.url == url);
            var first_match = new Article();
            if(matching_articles.Count() > 0){
                first_match = matching_articles.First();
            }
            string json = "";
            if(first_match.jsonstring != null){
                json = first_match.jsonstring;
            }else{
                json = ReadAPI.ExtractHtmlFromURL(url);
                first_match.jsonstring = JsonConvert.DeserializeObject(json).ToString();
                db.SaveChanges();
            }
            ViewBag.JSON = json;
            return View();
        }
    }
}