using Blog.Web.Models;

namespace Blog.Web.Services.IServices
{
    public interface IPostService : IBaseService
    {

        Task<T> GetAllPostAsync<T>();

        Task<T> GetPostByIdAsync<T>(int id);

        Task<T> CreatePostAsync<T>(PostDto postDto);

        Task<T> UpdatePostAsync<T>(PostDto postDto);

        Task<T> DeletePostAsync<T>(int id);

    }
}
