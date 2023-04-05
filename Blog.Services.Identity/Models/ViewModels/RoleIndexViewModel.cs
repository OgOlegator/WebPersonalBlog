using Microsoft.AspNetCore.Identity;

namespace Blog.Services.Identity.Models.ViewModels
{
    public class RoleIndexViewModel
    {
        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public IEnumerable<AppUser> Users { get; set; }

    }
}
