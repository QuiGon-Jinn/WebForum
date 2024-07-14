using Microsoft.EntityFrameworkCore;
using WebForumApi.Database.Models;

namespace WebForumApi.Database
{
    public class ForumRepo
    {
        private ForumDbContext db;

        public ForumRepo(ForumDbContext context)
        {
            db = context;
        }

        public async Task<List<Post>?> Get()
        {
            return await db.Posts.ToListAsync();
        }

        public async Task<Post> Add(string user, string text)
        {
            Post post = new Post()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                User = user,
                Text = text
            };

            db.Posts.Add(post);
            await db.SaveChangesAsync();
            return post;
        }
    }
}
