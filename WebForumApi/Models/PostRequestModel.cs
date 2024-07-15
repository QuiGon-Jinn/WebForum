using System.ComponentModel.DataAnnotations;

namespace WebForumApi.Models
{
    /// <summary>
    /// Post request model
    /// </summary>
    public class PostRequestModel
    {
        /// <summary>
        /// Text of the post
        /// </summary>
        public required string Text { get; set; }
    }
}
