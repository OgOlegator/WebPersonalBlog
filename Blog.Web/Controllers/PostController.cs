using Blog.Web.Extensions;
using Blog.Web.Models;
using Blog.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

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

        [HttpGet]
        [Authorize(Roles = $"{SD.UserRole}")]
        public async Task<IActionResult> PostCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{SD.UserRole}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostCreate(PostDto model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(String.Empty, "Некорректные данные");
                return View(model);
            }

            if (string.IsNullOrWhiteSpace(User.Identity.Name))
            {
                ModelState.AddModelError(String.Empty, "Не удалось получить данные пользователя. Повторите вход");
                return View(model);
            }

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            model.UserId = User.GetLoggedInUserId<string>();
            model.CreatedDate = DateTime.Now;

            var response = await _postService.CreatePostAsync<ResponseDto>(model, accessToken);

            if (response != null && response.IsSuccess)
                return RedirectToAction(nameof(PostUserIndex));
            else
            { 
                ModelState.AddModelError(String.Empty, "Не удалось создать пост");
                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.AdminRole}, {SD.UserRole}")]
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
        [Authorize(Roles = $"{SD.AdminRole}, {SD.UserRole}")]
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
                    return RedirectToAction(nameof(PostUserIndex));
                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Некорректные данные");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.AdminRole}, {SD.UserRole}")]
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
        [Authorize(Roles = $"{SD.AdminRole}, {SD.UserRole}")]
        public async Task<IActionResult> PostDelete(PostDto model)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var response = await _postService.DeletePostAsync<ResponseDto>(model.PostId, accessToken);

            if (response.IsSuccess)
                return RedirectToAction(nameof(PostUserIndex));

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.AdminRole}, {SD.UserRole}")]
        public async Task<IActionResult> PostUserIndex()
        {
            var listPosts = new List<PostDto>();

            var accessToken = await HttpContext.GetTokenAsync("access_token");

            ResponseDto response = null;

            //Для Админа должны быть показаны все посты
            if(User.GetLoggedInUserRole() == SD.UserRole)
                response = await _postService.GetPostByUserAsync<ResponseDto>(User.GetLoggedInUserId<string>(), accessToken);
            else if(User.GetLoggedInUserRole() == SD.AdminRole)
                response = await _postService.GetAllPostAsync<ResponseDto>();

            if (response != null && response.IsSuccess)
                listPosts = JsonConvert.DeserializeObject<List<PostDto>>(response.Result.ToString());

            return View(listPosts.OrderByDescending(post => post.CreatedDate).ToList());
        }
    }
}
