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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Post>().HasData(new Post
            //{
            //    PostId = 1,
            //    Name = "Тестовый пост 1",
            //    CreatedDate = DateTime.Now,
            //    Text = "...",
            //    UserId = "TestUser"
            //});

            //modelBuilder.Entity<Post>().HasData(new Post
            //{
            //    PostId = 2,
            //    Name = "Тестовый пост 2",
            //    CreatedDate = DateTime.Now,
            //    Text = "...",
            //    UserId = "TestUser"
            //});

            //modelBuilder.Entity<Post>().HasData(new Post
            //{
            //    PostId = 3,
            //    Name = "Тестовый пост 3",
            //    CreatedDate = DateTime.Now,
            //    Text = "...",
            //    UserId = "TestUser"
            //});

            //modelBuilder.Entity<Post>().HasData(new Post
            //{
            //    PostId = 4,
            //    Name = "Тестовый пост 4",
            //    CreatedDate = DateTime.Now,
            //    Text = "...",
            //    UserId = "TestUser"
            //});
        }
    }
}
