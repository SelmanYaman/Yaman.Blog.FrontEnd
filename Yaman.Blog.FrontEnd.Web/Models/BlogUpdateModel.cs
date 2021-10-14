using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yaman.Blog.FrontEnd.Web.Models
{
    public class BlogUpdateModel
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
