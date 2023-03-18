using Blog.Services.PostsAPI.Models.Dto;
using Blog.Services.PostsAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services.PostsAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/posts")]
    public class PostAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IPostRepository _repository;

        public PostAPIController(IPostRepository repository)
        {
            _response = new ResponseDto();
            _repository = repository;
        }

        /// <summary>
        /// Get all posts all users
        /// </summary>
        /// <returns>Returns PostList, state result, messages</returns>
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                _response.Result = await _repository.GetPosts();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Get concrete post by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return post, state result, messages</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                _response.Result = await _repository.GetPostById(id);
                if(_response.Result == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Post not found";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Get all posts by user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("{userName}")]
        public async Task<object> Get(string userName)
        {
            try
            {
                _response.Result = await _repository.GetPostsByUser(userName);
                if (_response.Result == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Posts not found";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return state result, messages</returns>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                _response.Result = await _repository.DeletePost(id);
                if(!(bool)_response.Result)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Не удалось удалить запись";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Create post
        /// </summary>
        /// <param name="postDto"></param>
        /// <returns>Return post, state result, messages</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<object> Post([FromBody] PostDto postDto)
        {
            try
            {
                _response.Result = await _repository.CreateUpdatePost(postDto);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        /// <summary>
        /// Change post
        /// </summary>
        /// <param name="postDto"></param>
        /// <returns>Return post, state result, messages</returns>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<object> Put([FromBody] PostDto postDto)
        {
            try
            {
                _response.Result = await _repository.CreateUpdatePost(postDto);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }
    }
}
