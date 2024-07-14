using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebForumApi.Database.Models
{
    public class Post
    {
        [Key, JsonIgnore]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? User { get; set; }
        public string? Text { get; set; }
        public List<Post> Comments { get; set;} = new List<Post>();
        public List<string> Likes { get; set; } = new List<string>();
        public List<string> Tags { get; set; } = new List<string>();
    }
}
