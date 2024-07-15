namespace WebForumApi.Models
{
    /// <summary>
    /// Tag request model
    /// </summary>
    public class TagRequestModel
    {
        /// <summary>
        /// Post Id
        /// </summary>
        public required Guid PostId { get; set; }

        /// <summary>
        /// Text of the tag
        /// </summary>
        public required string Text { get; set; }
    }
}
