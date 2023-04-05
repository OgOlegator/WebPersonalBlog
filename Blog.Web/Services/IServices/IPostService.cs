using Blog.Web.Models;

namespace Blog.Web.Services.IServices
{
    public interface IPostService : IBaseService
    {

        Task<T> GetAllPostAsync<T>();

        Task<T> GetPostByIdAsync<T>(int id, string token);

        Task<T> GetPostByUserAsync<T>(string userName, string token);

        Task<T> CreatePostAsync<T>(PostDto postDto, string token);

        Task<T> UpdatePostAsync<T>(PostDto postDto, string token);

        Task<T> DeletePostAsync<T>(int id, string token);

    }
}
