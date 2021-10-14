using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.Models;

namespace Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces
{
    public interface ICategoryApiService
    {
        Task<CategoryListModel> GetByIdAsync(int id);
        Task<List<CategoryListModel>> GetAllAsyc();
        Task<List<string>> CreateAsync(CategoryAddModel model);
        Task<List<string>> UpdateAsync(CategoryUpdateModel model);
        Task<string> DeleteAsync(int id);
    }
}
