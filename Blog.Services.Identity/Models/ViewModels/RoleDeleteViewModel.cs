using Microsoft.AspNetCore.Identity;

namespace Blog.Services.Identity.Models.ViewModels
{
    public class RoleDeleteViewModel
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public IEnumerable<string> Members { get; set; } = new List<string>();
    }
}
