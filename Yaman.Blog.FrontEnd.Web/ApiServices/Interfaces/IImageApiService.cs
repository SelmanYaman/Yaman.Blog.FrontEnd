using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces
{
    public interface IImageApiService
    {
        Task<string> GetBlogImageByIdAsync(int id);
    }
}
