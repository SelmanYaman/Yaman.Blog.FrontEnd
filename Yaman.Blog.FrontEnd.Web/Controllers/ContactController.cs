using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces;

namespace Yaman.Blog.FrontEnd.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUserApiService _userApiService;

        public ContactController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userApiService.GetByIdAsync(1));
        }
    }
}
