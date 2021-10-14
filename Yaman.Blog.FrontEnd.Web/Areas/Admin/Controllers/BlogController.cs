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
    public class BlogController : Controller
    {
        private readonly IBlogApiService _blogApiService;
        private readonly ICategoryApiService _categoryApiService;

        public BlogController(IBlogApiService blogApiService, ICategoryApiService categoryApiService)
        {
            _blogApiService = blogApiService;
            _categoryApiService = categoryApiService;
        }
        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            TempData["active"] = "blog";
            return View(await _blogApiService.GetAllAsync());
        }
        [JwtAuthorize]
        public async Task<IActionResult> Create()
        {
            TempData["active"] = "blog";
            ViewBag.Category = await _categoryApiService.GetAllAsyc();
            return View(new BlogAddModel());
        }
        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Create(BlogAddModel model)
        {
            TempData["active"] = "blog";
            var create = await _blogApiService.CreateAsync(model);
            if (create != null)
            {
                ViewBag.Category = await _categoryApiService.GetAllAsyc();
                foreach (var error in create)
                {
                    ModelState.AddModelError("", error);
                }
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [JwtAuthorize]
        public async Task<IActionResult> Update(int id)
        {
            TempData["active"] = "blog";
            var blogList = await _blogApiService.GetByIdAsync(id);
            ViewBag.Category = await _categoryApiService.GetAllAsyc();
            return View(new BlogUpdateModel()
            {
                Id = blogList.Id,
                CategoryId = blogList.CategoryId,
                Description = blogList.Description,
                ShortDescription = blogList.ShortDescription,
                Title = blogList.Title
            }) ;
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Update(BlogUpdateModel model)
        {
            TempData["active"] = "blog";
            var update = await _blogApiService.UpdateAsync(model);
            if (update != null)
            {
                ViewBag.Category = await _categoryApiService.GetAllAsyc();
                foreach (var error in update)
                {
                    ModelState.AddModelError("", error);
                }
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [JwtAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            TempData["active"] = "blog";
            var delete = await _blogApiService.DeleteAsync(id);
            if(delete != null)
            {
                TempData["errorMessage"] = delete;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
