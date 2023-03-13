using Blog.Services.PostsAPI.Models.Dto;
using Blog.Services.PostsAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services.PostsAPI.Controllers
{
    [ApiController]
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
                    _response.DisplayMessage = "Пост не найден";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

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
