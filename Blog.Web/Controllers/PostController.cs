using Blog.Web.Models;
using Blog.Web.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: PostController
        public async Task<IActionResult> PostIndex()
        {
            var listPosts = new List<PostDto>();

            var response = await _postService.GetAllPostAsync<ResponseDto>();

            if(response != null && response.IsSuccess)
            {
                listPosts = JsonConvert.DeserializeObject<List<PostDto>>(response.Result.ToString());
            }

            return View(listPosts);
        }

        public async Task<IActionResult> PostCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCreate(PostDto model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;

                var response = await _postService.CreatePostAsync<ResponseDto>(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(PostIndex));
                }
            }

            return View(model);
        }
    }
}
