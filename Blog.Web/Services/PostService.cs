﻿using Blog.Web.Models;
using Blog.Web.Services.IServices;
using Microsoft.Extensions.Options;

namespace Blog.Web.Services
{
    /// <summary>
    /// Сервис вызов АПИ Постов
    /// </summary>
    public class PostService : BaseService, IPostService
    {
        private readonly IHttpClientFactory _clientFactory;

        public PostService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreatePostAsync<T>(PostDto postDto, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                APIType = SD.ApiType.POST,
                Data = postDto,
                Url = SD.PostApiBase + "/api/posts",
                AccessToken = token
            });
        }

        public async Task<T> DeletePostAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                APIType = SD.ApiType.DELETE,
                Url = SD.PostApiBase + "/api/posts/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllPostAsync<T>()
        {
            return await SendAsync<T>(new ApiRequest()
            {
                APIType = SD.ApiType.GET,
                Url = SD.PostApiBase + "/api/posts",
                AccessToken = ""
            });
        }

        public async Task<T> GetPostByIdAsync<T>(int id, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                APIType = SD.ApiType.GET,
                Url = SD.PostApiBase + "/api/posts/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetPostByUserAsync<T>(string userName, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                APIType = SD.ApiType.GET,
                Url = SD.PostApiBase + "/api/posts/" + "user/" + userName,
                AccessToken = token
            });
        }

        public async Task<T> UpdatePostAsync<T>(PostDto postDto, string token)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                APIType = SD.ApiType.PUT,
                Data = postDto,
                Url = SD.PostApiBase + "/api/posts",
                AccessToken = token
            });
        }
    }
}
