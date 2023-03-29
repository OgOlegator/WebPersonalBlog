using Blog.Services.Identity.Models;
using Blog.Services.Identity.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.Identity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleAdminController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.ToList();

            var listViewModel = new List<RoleIndexViewModel>();

            foreach(var role in roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(role.Name);

                listViewModel.Add(new RoleIndexViewModel { 
                    RoleId =  role.Id, 
                    RoleName = role.Name, 
                    Users = users 
                });
            }

            return View(listViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string roleId)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == roleId);

            if(role == null)
                return NotFound();

            var viewModel = new RoleEditViewModel
            {
                Role = role,
            };

            viewModel.Members = await _userManager.GetUsersInRoleAsync(role.Name);
            viewModel.NonMembers = _userManager.Users.ToList().Except(viewModel.Members);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Error", new string[] { "Роль не найдена" });

            foreach (string userId in viewModel.IdsToAdd ?? new string[] { })
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

                var result = await _userManager.AddToRoleAsync(user, viewModel.RoleName);

                if (!result.Succeeded)
                    return View("Error", result.Errors);
            }

            foreach (string userId in viewModel.IdsToDelete ?? new string[] { })
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);

                var result = await _userManager.RemoveFromRoleAsync(user, viewModel.RoleName);

                if (!result.Succeeded)
                    return View("Error", result.Errors);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleCreateViewModel viewModel)
        {
            var role = new IdentityRole
            {
                NormalizedName = viewModel.RoleName,
                Name = viewModel.RoleName,
            };
            
            var result = await _roleManager.CreateAsync(role);

            if(!result.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Не удалось создать роль");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
