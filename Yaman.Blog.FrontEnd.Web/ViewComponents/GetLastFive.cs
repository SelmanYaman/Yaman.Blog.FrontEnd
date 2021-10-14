using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces;

namespace Yaman.Blog.FrontEnd.Web.ViewComponents
{
    public class GetLastFive : ViewComponent
    {
        private readonly IBlogApiService _blogApiService;

        public GetLastFive(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_blogApiService.GetLastFiveAsync().Result);
        }
    }
}
