using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewlyReadCore.SQLite;

namespace NewlyReadCore{
    public class CategoriesController : Controller{

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