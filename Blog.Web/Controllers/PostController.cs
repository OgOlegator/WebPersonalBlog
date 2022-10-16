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

        public async Task<IActionResult> PostIndex()
        {
            var listPosts = new List<PostDto>();

            var response = await _postService.GetAllPostAsync<ResponseDto>();

            if(response != null && response.IsSuccess)
            {
                listPosts = JsonConvert.DeserializeObject<List<PostDto>>(response.Result.ToString());
            }

            return View(listPosts.OrderByDescending(post => post.CreatedDate).ToList());
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

        public async Task<IActionResult> PostEdit(int postId)
        {
            var response = await _postService.GetPostByIdAsync<ResponseDto>(postId);

            if(response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<PostDto>(response.Result.ToString());
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEdit(PostDto model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;

                var response = await _postService.UpdatePostAsync<ResponseDto>(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(PostIndex));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> PostDelete(int postId)
        {
            var response = await _postService.GetPostByIdAsync<ResponseDto>(postId);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<PostDto>(response.Result.ToString());
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostDelete(PostDto model)
        {
            var response = await _postService.DeletePostAsync<ResponseDto>(model.PostId);

            if (response.IsSuccess)
                return RedirectToAction(nameof(PostIndex));

            return View(model);
        }
    }
}
