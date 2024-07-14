using Microsoft.EntityFrameworkCore;
using WebForumApi.Database.Configurations;
using WebForumApi.Database.Models;

namespace WebForumApi.Database
{
    public class ForumDbContext(DbContextOptions<ForumDbContext> options) : DbContext(options)
    {
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PostConfiguration());
        }
    }
}
