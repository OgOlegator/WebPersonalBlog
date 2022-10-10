using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class Post
    {

        public int PostId { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }


        [Display(Name = "Create Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }


        [StringLength(300, MinimumLength = 0)]
        public string Text { get; set; }

    }
}
