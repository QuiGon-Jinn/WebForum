using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebForumApi.Database.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? User { get; set; }
        public string? Text { get; set; }
        public List<string> Likes { get; set; } = [];

        public List<string> Tags { get; set; } = [];
        public List<Post> Comments { get; set;} = [];

        [JsonIgnore]
        public Post? ParentPost { get; set; }

        [JsonIgnore]
        public Guid? ParentPostId { get; set; }

        [JsonIgnore]
        public int LikesCount { get { return Likes.Count; } }

        [JsonIgnore]
        public int CommentsCount { get { return Comments.Count; } }
    }
}
