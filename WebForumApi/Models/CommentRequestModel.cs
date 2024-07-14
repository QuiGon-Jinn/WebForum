namespace WebForumApi.Models
{
    public class CommentRequestModel : PostRequestModel
    {
        public required Guid PostId { get; set; }
    }
}
