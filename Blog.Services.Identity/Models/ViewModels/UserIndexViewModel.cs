using Microsoft.AspNetCore.Identity;

namespace Blog.Services.Identity.Models.ViewModels
{
    public class UserIndexViewModel
    {

        public AppUser User { get; set; }

        public IEnumerable<string> Roles { get; set; }

    }
}
