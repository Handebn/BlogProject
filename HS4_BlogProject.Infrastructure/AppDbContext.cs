using HS4_BlogProject.Domain.Entities;
using HS4_BlogProject.Infrastructure.EntityTypeConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HS4_BlogProject.Infrastructure
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Post> Posts { get; set; }        
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new GenreConfig());
            builder.ApplyConfiguration(new PostConfig());
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new AuthorConfig());

            base.OnModelCreating(builder);
        }
    }
}
