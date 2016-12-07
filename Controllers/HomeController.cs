using Microsoft.AspNetCore.Mvc;
using NewlyReadCore.SQLite;

namespace NewlyReadCore.Controllers {
    public class HomeController : Controller {
        public IActionResult Index(){
            var db = new MyDBContext();
            ViewBag.DB = ReadAPI.SummarizeLatestArticles(1);
            return View();
        }
    }
}