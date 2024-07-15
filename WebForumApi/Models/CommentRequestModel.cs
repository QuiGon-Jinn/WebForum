namespace WebForumApi.Models
{
    /// <summary>
    /// Comment request model
    /// </summary>
    public class CommentRequestModel : PostRequestModel
    {
        /// <summary>
        /// Post Id
        /// </summary>
        public required Guid PostId { get; set; }
    }
}
