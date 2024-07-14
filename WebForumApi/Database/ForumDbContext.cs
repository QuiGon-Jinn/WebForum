using Microsoft.EntityFrameworkCore;
using WebForumApi.Database.Models;

namespace WebForumApi.Database
{
    public class ForumDbContext :DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options) { }
    }
}
