using System.ComponentModel.DataAnnotations;

namespace Blog.Services.PostsAPI.Models.Dto
{
    public class PostDto
    {

        public int PostId { get; set; }

        
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }


        public string Text { get; set; }

    }
}
