using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebForumApi.Database.Models
{
    /// <summary>
    /// Post (or comment)
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Unique id for this post / comment
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        
        /// <summary>
        /// Date and time post / comment was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User / author of this post / comment
        /// </summary>
        public string? User { get; set; }
        
        /// <summary>
        /// Content of the post / comment
        /// </summary>
        public string? Text { get; set; }
        
        /// <summary>
        /// List of users who like this
        /// </summary>
        public List<string> Likes { get; set; } = [];

        /// <summary>
        /// List of tags this item received
        /// </summary>
        public List<string> Tags { get; set; } = [];
        
        /// <summary>
        /// List of copmments this item received
        /// </summary>
        public List<Post> Comments { get; set;} = [];

        [JsonIgnore]
        public Post? ParentPost { get; set; }

        [JsonIgnore]
        public Guid? ParentPostId { get; set; }

        /// <summary>
        /// Number of likes received
        /// </summary>
        [JsonIgnore]        
        public int LikesCount { get { return Likes.Count; } }

        /// <summary>
        /// Number of comments received
        /// </summary>
        [JsonIgnore]
        public int CommentsCount { get { return Comments.Count; } }
    }
}
