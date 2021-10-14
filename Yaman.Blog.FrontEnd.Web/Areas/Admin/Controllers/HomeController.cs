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
    public class HomeController : Controller
    {
        private readonly ICommentApiService _commentApiService;

        public HomeController(ICommentApiService commentApiService)
        {
            _commentApiService = commentApiService;
        }

        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            TempData["active"] = "home";
            return View(await _commentApiService.GetUnApprovedCommentsAsync());
        }
        public async Task<IActionResult> Approve(int id)
        {
            await _commentApiService.UpdateCommentIsApprovedAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _commentApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult SignOutt()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
