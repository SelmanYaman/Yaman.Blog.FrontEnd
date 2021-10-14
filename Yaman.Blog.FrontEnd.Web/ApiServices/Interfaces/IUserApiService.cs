using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.Models;

namespace Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces
{
    public interface IUserApiService
    {
        Task<UserModel> GetByIdAsync(int id);
        Task<List<string>> UpdateAsync(UserModel model);
        Task<bool> SignIn(AppUserLoginModel model);
    }
}
