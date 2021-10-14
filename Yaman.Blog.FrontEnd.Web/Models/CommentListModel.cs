using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yaman.Blog.FrontEnd.Web.Models
{
    public class CommentListModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public DateTime PostedTime { get; set; }
        public int? ParentCommentId { get; set; }

        public List<CommentListModel> SubComments { get; set; }

        public int BlogId { get; set; }
    }
}
