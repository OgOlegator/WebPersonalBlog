using System.ComponentModel.DataAnnotations;

namespace Blog.Services.Identity.Models.ViewModels
{
    public class UserDeleteViewModel
    {

        public string UserId { get; set; }

        public string UserName { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
