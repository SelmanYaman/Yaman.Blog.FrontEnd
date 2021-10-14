using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces;
using Yaman.Blog.FrontEnd.Web.Models;

namespace Yaman.Blog.FrontEnd.Web.ApiServices.Concrete
{
    public class BlogApiManager : IBlogApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/blog/");
        }

        public async Task<List<BlogListModel>> GetAllAsync()
        {
            var responseData = await _httpClient.GetAsync("");
            if (responseData.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseData.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<BlogListModel> GetByIdAsync(int id)
        {
            var responseData = await _httpClient.GetAsync($"{id}");
            if (responseData.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BlogListModel>(await responseData.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<string>> CreateAsync(BlogAddModel model)
        {
            MultipartFormDataContent formData = new();
            if (model.Image != null)
            {
                //var bytes = await System.IO.File.ReadAllBytesAsync(model.Image.FileName);
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                ByteArrayContent byteArray = new ByteArrayContent(bytes);
                byteArray.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.Image.ContentType);
                formData.Add(byteArray, nameof(BlogAddModel.Image), model.Image.FileName);
            }
            //model.AppUserId = 1;
            if (model.Description == null) model.Description = "";
            if (model.ShortDescription == null) model.ShortDescription = "";
            if (model.Title == null) model.Title = "";
            //formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogAddModel.AppUserId));
            formData.Add(new StringContent(model.CategoryId.ToString()), nameof(BlogAddModel.CategoryId));
            formData.Add(new StringContent(model.ShortDescription), nameof(BlogAddModel.ShortDescription));
            formData.Add(new StringContent(model.Description), nameof(BlogAddModel.Description));
            formData.Add(new StringContent(model.Title), nameof(BlogAddModel.Title));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var responseMessage = await _httpClient.PostAsync("", formData);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<string>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<string>> UpdateAsync(BlogUpdateModel model)
        {
            MultipartFormDataContent formData = new();
            if (model.Image != null)
            {
                var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                var bytes = stream.ToArray();
                ByteArrayContent byteArray = new ByteArrayContent(bytes);
                byteArray.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(model.Image.ContentType);
                formData.Add(byteArray, nameof(BlogUpdateModel.Image), model.Image.FileName);
            }
            //model.AppUserId = 1;
            if (model.Description == null) model.Description = "";
            if (model.ShortDescription == null) model.ShortDescription = "";
            if (model.Title == null) model.Title = "";
            formData.Add(new StringContent(model.Id.ToString()), nameof(BlogUpdateModel.Id));
            formData.Add(new StringContent(model.CategoryId.ToString()), nameof(BlogAddModel.CategoryId));
            //formData.Add(new StringContent(model.AppUserId.ToString()), nameof(BlogUpdateModel.AppUserId));
            formData.Add(new StringContent(model.ShortDescription), nameof(BlogUpdateModel.ShortDescription));
            formData.Add(new StringContent(model.Description), nameof(BlogUpdateModel.Description));
            formData.Add(new StringContent(model.Title), nameof(BlogUpdateModel.Title));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var responseMessage = await _httpClient.PutAsync("", formData);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<string>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<string> DeleteAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var responseMessage = await _httpClient.DeleteAsync($"{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<List<BlogListModel>> GetLastFiveAsync()
        {
            var responseMessage = await _httpClient.GetAsync("GetLastFive");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<BlogListModel>> GetAllByCategoryIdAsync(int id)
        {
            var responseMessage = await _httpClient.GetAsync($"GetAllByCategoryId/{id}");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<BlogListModel>> SearchAsync(string s)
        {
            var responseMessage = await _httpClient.GetAsync($"Search?s={s}");
            if(responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BlogListModel>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }
    }
}
