using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using WebForumApi.Database.Models;

namespace WebForumApi.Database
{
    public class ForumRepo(ForumDbContext context)
    {
        private readonly ForumDbContext db = context;

        public async Task<List<Post>?> Get(DateTime fromDate, DateTime toDate, string user, string tag, string orderBy, 
            int? page = null, int? pageSize = null)
        {
            var posts = db.Posts.Where(x => x.ParentPostId != null 
                || x.ParentPostId == null && x.CreatedDate >= fromDate && x.CreatedDate < toDate
                && (string.IsNullOrEmpty(user) || EF.Functions.Like((x.User ?? ""), $"%{user}%"))
                //&& (string.IsNullOrEmpty(tag) || x.Tags.Contains(tag))
            );

            var result = (await posts.ToListAsync()).FindAll(x => x.ParentPostId == null 
                && (string.IsNullOrEmpty(tag) || x.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)));

            if (!string.IsNullOrEmpty(orderBy))
            {
                var orderByArgs = orderBy.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var propertyInfo = typeof(Post).GetProperty(orderByArgs[0], BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    if (orderByArgs.Length > 1 && orderByArgs[1].Contains("desc", StringComparison.InvariantCultureIgnoreCase))
                    {
                        result = [.. result.OrderByDescending(x => propertyInfo.GetValue(x))];
                    }
                    else
                    {
                        result = [.. result.OrderBy(x => propertyInfo.GetValue(x))];
                    }
                }
            }

            if (page != null && pageSize != null)
            {
                result = [.. result.Skip(((page ?? 0) - 1) * (pageSize ?? 0)).Take(pageSize ?? 0)];
            }

            return result; 
        }

        public async Task<Post> Add(string user, string text)
        {
            var post = new Post()
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

        public async Task<Post?> Comment(Guid postId, string user, string text)
        {
            var parent = await db.Posts.FirstOrDefaultAsync(x => x.Id == postId)
                ?? throw new KeyNotFoundException("Post could not be found");            

            var comment = new Post()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                User = user,
                Text = text,
                ParentPost = parent,
                ParentPostId = parent.Id
            };

            db.Posts.Add(comment);
            parent.Comments.Add(comment);            
            db.Posts.Update(parent);
            await db.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> Like(Guid postId, string user, bool remove = false)
        {
            var post = await db.Posts.FirstOrDefaultAsync(x => x.Id == postId)
                ?? throw new KeyNotFoundException("Post could not be found");

            if (post.User == user)
            {
                throw new InvalidOperationException("User can't like own posts");
            }

            if(post.Likes.Contains(user) && !remove
                || !post.Likes.Contains(user) && remove)
            {
                return false;
            }

            if (remove)
            {
                post.Likes.Remove(user);
            }
            else
            {
                post.Likes.Add(user);
            }

            db.Posts.Update(post);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Tag(Guid postId, string text)
        {
            var post = await db.Posts.FirstOrDefaultAsync(x => x.Id == postId) 
                ?? throw new KeyNotFoundException("Post could not be found");
            
            if (post.Tags.Contains(text))
            {
                return false;
            }

            post.Tags.Add(text);
            db.Posts.Update(post);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
