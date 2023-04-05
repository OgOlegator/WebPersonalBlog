using Blog.Services.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Services.Identity.DbContexts
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
        }

    }
}
