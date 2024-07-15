namespace WebForumApi.Models
{
    public class ViewRequestModel
    {
        public DateTime FromDate { get; set; } = DateTime.MinValue;
        public DateTime ToDate { get; set; } = DateTime.MaxValue;
        public string? Author { get; set; }

        /// <summary>
        /// Example: "CommentsCount desc" or "Author"
        /// </summary>
        public string? OrderBy { get; set; }
        public int? PageNo { get; set; }
        public int? PageSize { get; set; }
    }
}
