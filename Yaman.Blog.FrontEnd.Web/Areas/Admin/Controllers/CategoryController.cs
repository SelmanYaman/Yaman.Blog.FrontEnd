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
    public class CategoryController : Controller
    {
        private readonly ICategoryApiService _categoryApiService;

        public CategoryController(ICategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }
        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            TempData["active"] = "category";
            return View(await _categoryApiService.GetAllAsyc());
        }
        [JwtAuthorize]
        public IActionResult Create()
        {
            TempData["active"] = "category";
            return View(new CategoryAddModel());
        }
        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Create(CategoryAddModel model)
        {
            TempData["active"] = "category";
            var create = await _categoryApiService.CreateAsync(model);
            if(create != null)
            {
                foreach (var item in create)
                {
                    ModelState.AddModelError("", item);
                }
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [JwtAuthorize]
        public async Task<IActionResult> Update(int id)
        {
            TempData["active"] = "category";
            var categoryList = await _categoryApiService.GetByIdAsync(id);
            return View(new CategoryUpdateModel
            {
                Id = categoryList.Id,
                Name = categoryList.Name
            });
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Update(CategoryUpdateModel model)
        {
            TempData["active"] = "category";
            var update = await _categoryApiService.UpdateAsync(model);
            if(update != null)
            {
                foreach (var item in update)
                {
                    ModelState.AddModelError("", item);
                }
                return View(model);
            }    
            return RedirectToAction("Index");
        }
        [JwtAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            TempData["active"] = "category";
            var delete = await _categoryApiService.DeleteAsync(id);
            if(delete != null)
            {
                TempData["errorMessage"] = delete;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
