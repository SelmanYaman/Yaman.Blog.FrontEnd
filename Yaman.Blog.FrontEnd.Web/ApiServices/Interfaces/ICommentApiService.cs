using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.Models;

namespace Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces
{
    public interface ICommentApiService
    {
        Task<List<string>> AddToCommentAsync(CommentAddModel model);
        Task<List<CommentListModel>> GetCommentAsync(int blogId, int? parentCommentId);
        Task<List<CommentListModel>> GetUnApprovedCommentsAsync();
        Task<List<string>> UpdateCommentIsApprovedAsync(int id);
        Task<string> DeleteAsync(int id);
    }
}
