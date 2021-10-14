using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces;
using Yaman.Blog.FrontEnd.Web.Models;

namespace Yaman.Blog.FrontEnd.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        private readonly ICommentApiService _commentApiService;

        public HomeController(IBlogApiService blogApiService, ICommentApiService commentApiService)
        {
            _blogApiService = blogApiService;
            _commentApiService = commentApiService;
        }

        public async Task<IActionResult> Index(int? categoryId, string categoryName, string s)
        {
            if (categoryId.HasValue)
            {
                ViewBag.CategoryName = categoryName;
                var data = await _blogApiService.GetAllByCategoryIdAsync((int)categoryId);
                return View(data);
            }
            if (!string.IsNullOrWhiteSpace(s))
            {
                ViewBag.Search = s;
                var data = await _blogApiService.SearchAsync(s);
                return View(data);
            }
            return View(await _blogApiService.GetAllAsync());
        }

        public async Task<IActionResult> Details(int blogId)
        {
            ViewBag.Comments = await _commentApiService.GetCommentAsync(blogId, null);
            return View(await _blogApiService.GetByIdAsync(blogId));
        }

        [HttpPost]
        public async Task<IActionResult> AddToComment(CommentAddModel model)
        {
            await _commentApiService.AddToCommentAsync(model);
            return RedirectToAction("Details", new { blogId = model.BlogId });
        }
    }
}
