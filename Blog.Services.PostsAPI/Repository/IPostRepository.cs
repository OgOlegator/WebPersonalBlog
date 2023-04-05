using Blog.Services.PostsAPI.Models.Dto;

namespace Blog.Services.PostsAPI.Repository
{
    public interface IPostRepository
    {

        Task<IEnumerable<PostDto>> GetPosts();

        Task<PostDto> GetPostById(int id);

        Task<IEnumerable<PostDto>> GetPostsByUser(string userId);

        Task<PostDto> CreateUpdatePost(PostDto postDto);

        Task<bool> DeletePost(int postId);

    }
}
