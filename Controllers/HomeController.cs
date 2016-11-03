using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace NewlyReadCore.Controllers {
    public class HomeController : Controller {
        public IActionResult Index(){
            return View();
        }

        public IActionResult About(){
            return View("About");
        }
    }
}