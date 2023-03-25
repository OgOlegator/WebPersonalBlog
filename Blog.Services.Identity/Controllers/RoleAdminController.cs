using Blog.Services.Identity.Models;
using Blog.Services.Identity.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services.Identity.Controllers
{
    public class RoleAdminController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles.ToList();

            var listViewModel = new List<RoleAdminViewModel>();

            foreach(var role in roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(role.Name);

                listViewModel.Add(new RoleAdminViewModel { 
                    RoleId =  role.Id, 
                    RoleName = role.Name, 
                    Users = users 
                });
            }

            return View(listViewModel);
        }
    }
}
