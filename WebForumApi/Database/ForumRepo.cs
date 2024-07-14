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

        
    }
}
