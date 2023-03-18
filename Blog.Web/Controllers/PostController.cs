using Blog.Web.Models;
using Blog.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
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
                if (string.IsNullOrWhiteSpace(User.Identity.Name))
                {
                    ModelState.AddModelError(String.Empty, "Не удалось получить данные пользователя. Повторите вход");
                    return View(model);
                }

                var accessToken = await HttpContext.GetTokenAsync("access_token");

                model.UserName = User.Identity.Name;
                model.CreatedDate = DateTime.Now;

                var response = await _postService.CreatePostAsync<ResponseDto>(model, accessToken);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(PostIndex));
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Некорректные данные");
            }

            return View(model);
        }

        public async Task<IActionResult> PostEdit(int postId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _postService.GetPostByIdAsync<ResponseDto>(postId, accessToken);

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
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                model.CreatedDate = DateTime.Now;

                var response = await _postService.UpdatePostAsync<ResponseDto>(model, accessToken);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(PostIndex));
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Некорректные данные");
            }

            return View(model);
        }

        public async Task<IActionResult> PostDelete(int postId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _postService.GetPostByIdAsync<ResponseDto>(postId, accessToken);

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
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _postService.DeletePostAsync<ResponseDto>(model.PostId, accessToken);

            if (response.IsSuccess)
                return RedirectToAction(nameof(PostIndex));

            return View(model);
        }
    }
}
