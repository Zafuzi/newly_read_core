using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stormpath.SDK;
using Stormpath.SDK.Account;

namespace NewlyReadCore.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UpdatePasswordAsync(string newPassword)
        {
            await UpdatePassword(newPassword);
            return View("Index");
        }

        private static readonly Lazy<IAccount> account;
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(string newPassword)
        {
            if (account.Value != null)
            {
                var stormpathAccount = account.Value;
                stormpathAccount.SetPassword(newPassword);
                await stormpathAccount.SaveAsync();
            }

            return RedirectToAction("Index");
        }
    }
}