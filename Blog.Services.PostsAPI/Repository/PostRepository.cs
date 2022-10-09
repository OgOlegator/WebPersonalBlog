using AutoMapper;
using Blog.Services.PostsAPI.DbContexts;
using Blog.Services.PostsAPI.Models.Dto;
using Blog.Services.PostsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.PostsAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public PostRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PostDto> CreateUpdatePost(PostDto postDto)
        {
            var post = _mapper.Map<PostDto, Post>(postDto);

            if(post.PostId > 0)
            {
                _db.Update(post);
            }
            else
            {
                _db.Posts.Add(post);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Post, PostDto>(post);
        }

        public async Task<bool> DeletePost(int postId)
        {
            try
            {
                var post = await _db.Posts.FirstOrDefaultAsync(post => post.PostId == postId);

                if (post == null)
                {
                    return false;
                }

                _db.Posts.Remove(post);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PostDto> GetPostById(int postId)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(post => post.PostId == postId);
            return _mapper.Map<PostDto>(post);
        }

        public async Task<IEnumerable<PostDto>> GetPosts()
        {
            var listPosts = await _db.Posts.ToListAsync();
            return _mapper.Map<List<PostDto>>(listPosts);
        }
    }
}
