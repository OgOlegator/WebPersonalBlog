using System.ComponentModel.DataAnnotations;

namespace Blog.Services.Identity.Models.ViewModels
{
    public class RoleModificationViewModel
    {

        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }

    }
}
