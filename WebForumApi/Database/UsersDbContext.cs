using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebForumApi.Database
{
    public class UsersDbContext : IdentityDbContext<IdentityUser>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }
    }
}
