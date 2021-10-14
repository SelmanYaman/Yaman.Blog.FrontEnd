using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces;
using Yaman.Blog.FrontEnd.Web.Models;

namespace Yaman.Blog.FrontEnd.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserApiService _userApiService;

        public AccountController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginModel model)
        {
            var responseMessage = await _userApiService.SignIn(model);
            if (responseMessage)
            {
                return RedirectToAction("Index", "Home", new { @area = "Admin" });
            }
            ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Yanlış...");
            return View();
        }
    }
}
