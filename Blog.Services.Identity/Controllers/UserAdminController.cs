using Blog.Services.Identity.Models;
using Blog.Services.Identity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Blog.Services.Identity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public UserAdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new List<UserIndexViewModel>();

            foreach(var user in _userManager.Users)
                viewModel.Add(new UserIndexViewModel
                {
                    User = user,
                    Roles = await _userManager.GetRolesAsync(user),
                });

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return NotFound();

            var viewModel = new UserEditViewModel 
            { 
                UserId = userId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel viewModel)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == viewModel.UserId);

            if (user == null)
                return NotFound();

            user.UserName = viewModel.UserName;
            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Не удалось обновить информацию о пользователе");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return NotFound();

            var viewModel = new UserDeleteViewModel
            {
                UserId = userId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteViewModel viewModel)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == viewModel.UserId);

            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Не удалось удалить пользователя");
            return View(viewModel);
        }
    }
}
