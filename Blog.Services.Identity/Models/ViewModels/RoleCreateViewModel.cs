using System.ComponentModel.DataAnnotations;

namespace Blog.Services.Identity.Models.ViewModels
{
    public class RoleCreateViewModel
    {

        [Required]
        public string RoleName { get; set; }

    }
}
