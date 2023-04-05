using System.ComponentModel.DataAnnotations;

namespace Blog.Services.Identity.Models.ViewModels
{
    public class UserEditViewModel
    {

        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
