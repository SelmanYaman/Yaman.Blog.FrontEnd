using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.Models;

namespace Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces
{
    public interface IBlogApiService
    {
        Task<List<BlogListModel>> GetAllAsync();
        Task<BlogListModel> GetByIdAsync(int id);
        Task<List<BlogListModel>> GetLastFiveAsync();
        Task<List<BlogListModel>> GetAllByCategoryIdAsync(int id);
        Task<List<BlogListModel>> SearchAsync(string s);
        Task<List<string>> CreateAsync(BlogAddModel model);
        Task<List<string>> UpdateAsync(BlogUpdateModel model);
        Task<string> DeleteAsync(int id);

    }
}
