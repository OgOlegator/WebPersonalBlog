using Blog.Services.PostsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services.PostsAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; }
    }
}
