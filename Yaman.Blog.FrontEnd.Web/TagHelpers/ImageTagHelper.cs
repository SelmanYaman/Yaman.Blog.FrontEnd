using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces;

namespace Yaman.Blog.FrontEnd.Web.TagHelpers
{
    [HtmlTargetElement("getBlogImage")]
    public class ImageTagHelper : TagHelper
    {
        private readonly IImageApiService _imageApiService;

        public ImageTagHelper(IImageApiService imageApiService)
        {
            _imageApiService = imageApiService;
        }

        public int Id { get; set; }
        public int ImageBoyut { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var blob = await _imageApiService.GetBlogImageByIdAsync(Id);
            string html = string.Empty;
            if(ImageBoyut == 1)
            {
                html = $"<img src='{blob}' class='img-fluid' style='height:240px;width:370px;'>";
            }
            else
            {
                html = $"<img src='{blob}' class='img-fluid' style='height:400px;width:750px;'>";
            }
            output.Content.SetHtmlContent(html);
        }
    }
}
