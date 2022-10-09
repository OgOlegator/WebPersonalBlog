﻿using Blog.Web.Models;

namespace Blog.Web.Services.IServices
{
    public interface IBaseService : IDisposable
    {

        ResponseDto responseModel { get; set; }

        Task<T> SendAsync<T>(ApiRequest apiRequest);

    }
}
