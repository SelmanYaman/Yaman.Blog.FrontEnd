using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.FrontEnd.Web.ApiServices.Interfaces;
using Yaman.Blog.FrontEnd.Web.Models;

namespace Yaman.Blog.FrontEnd.Web.ApiServices.Concrete
{
    public class CommentApiManager : ICommentApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentApiManager(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/comment/");
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<string>> AddToCommentAsync(CommentAddModel model)
        {
            var JsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<string>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var responseMessage = await _httpClient.DeleteAsync($"{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<List<CommentListModel>> GetCommentAsync(int blogId, int? parentCommentId)
        {
            var responseData = await _httpClient.GetAsync($"{blogId}/GetComments?parentCommentId={parentCommentId}");
            if (responseData.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CommentListModel>>(await responseData.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<List<CommentListModel>> GetUnApprovedCommentsAsync()
        {
            var responseData = await _httpClient.GetAsync("");
            if(responseData.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<CommentListModel>>(await responseData.Content.ReadAsStringAsync());
            }
            return null;
        }

        public async Task<List<string>> UpdateCommentIsApprovedAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.Session.GetString("token"));
            var responseMessage = await _httpClient.GetAsync($"{id}");
            if (!responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<string>>(await responseMessage.Content.ReadAsStringAsync());
            }
            return null;
        }
    }
}
