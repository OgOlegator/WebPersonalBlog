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

        /// <summary>
        /// Создание или изменение поста
        /// </summary>
        /// <param name="postDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Удаление поста
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получение конкретного поста по ИД
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public async Task<PostDto> GetPostById(int postId)
        {
            var post = await _db.Posts.FirstOrDefaultAsync(post => post.PostId == postId);
            return _mapper.Map<PostDto>(post);
        }

        /// <summary>
        /// Получение всех постов
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PostDto>> GetPosts()
        {
            var listPosts = await _db.Posts.ToListAsync();
            return _mapper.Map<List<PostDto>>(listPosts);
        }

        /// <summary>
        /// Получение постов по Id пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PostDto>> GetPostsByUser(string userId)
        {
            var listPosts = await _db.Posts.Where(post => post.UserId == userId).ToListAsync();
            return _mapper.Map<List<PostDto>>(listPosts);
        }
    }
}
