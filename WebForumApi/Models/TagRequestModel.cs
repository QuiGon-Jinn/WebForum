namespace WebForumApi.Models
{
    public class TagRequestModel
    {
        public required Guid PostId { get; set; }
        public required string Text { get; set; }
    }
}
