using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class PostDto
    {

        public int PostId { get; set; }

        
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }


        public string Text { get; set; }

    }
}
