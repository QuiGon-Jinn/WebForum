using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebForumApi.Database.Models;

namespace WebForumApi.Database.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .HasOne(x => x.ParentPost)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ParentPostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
