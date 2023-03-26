using Microsoft.AspNetCore.Identity;

namespace Blog.Services.Identity.Models.ViewModels
{
    public class RoleEditViewModel
    {

        public IdentityRole Role { get; set; }

        public IEnumerable<AppUser> Members { get; set; } = new List<AppUser>();

        public IEnumerable<AppUser> NonMembers { get; set; } = new List<AppUser>();

    }
}
