using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces;
using Yaman.Blog.FrontEnd.Web.Filters;
using Yaman.Blog.FrontEnd.Web.Models;

namespace Yaman.Blog.FrontEnd.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserApiService _userApiService;

        public UserController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }
        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            TempData["active"] = "user";
            return View(await _userApiService.GetByIdAsync(1));
        }
        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Index(UserModel model)
        {
            TempData["active"] = "user";
            var update = await _userApiService.UpdateAsync(model);
            if (update != null)
            {
                foreach (var error in update)
                {
                    ModelState.AddModelError("", error);
                }
                return View(model);
            }
            return View();
        }
    }
}
