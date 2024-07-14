using System.ComponentModel.DataAnnotations;

namespace WebForumApi.Models
{
    public class PostRequestModel
    {
        public required string Text { get; set; }
    }
}
