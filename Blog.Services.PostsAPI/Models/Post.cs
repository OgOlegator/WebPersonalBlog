using System.ComponentModel.DataAnnotations;

namespace Blog.Services.PostsAPI.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

       
        public string Text { get; set; }

    }
}
