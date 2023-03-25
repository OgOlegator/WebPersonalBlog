using Blog.Services.Identity.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.Services.Identity.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            SignInManager<AppUser> signInManager, 
            UserManager<AppUser> userManager, 
            IIdentityServerInteractionService interactionService,
            RoleManager<IdentityRole> roleInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
            _roleManager = roleInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if(!ModelState.IsValid)
                return View(viewModel);

            var user = await _userManager.FindByNameAsync(viewModel.Username);

            if(user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            
            if(result.Succeeded)
            {
                var userRole = await _userManager.GetRolesAsync(user);

                await _userManager.AddClaimsAsync(user, new Claim[]{ new Claim(JwtClaimTypes.Role, userRole.FirstOrDefault()) });

                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Login error");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var user = new AppUser
            {
                UserName = viewModel.Username,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                if (!_roleManager.RoleExistsAsync(viewModel.RoleName).GetAwaiter().GetResult())
                {
                    var userRole = new IdentityRole
                    {
                        Name = viewModel.RoleName,
                        NormalizedName = viewModel.RoleName,
                    };
                    await _roleManager.CreateAsync(userRole);
                }

                await _userManager.AddToRoleAsync(user, viewModel.RoleName);

                await _userManager.AddClaimsAsync(user, new Claim[]{
                            new Claim(JwtClaimTypes.Name, viewModel.Username),
                            new Claim(JwtClaimTypes.FamilyName, viewModel.FirstName),
                            new Claim(JwtClaimTypes.GivenName, viewModel.LastName),
                            new Claim(JwtClaimTypes.Role, viewModel.RoleName) });

                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Erorr occurred");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
