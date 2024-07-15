using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebForumApi.Models
{
    /// <summary>
    /// View request model
    /// </summary>
    public class ViewRequestModel
    {
        /// <summary>
        /// From date and time to filter on
        /// </summary>
        public DateTime FromDate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// To date and time to filter on
        /// </summary>
        public DateTime ToDate { get; set; } = DateTime.MaxValue;

        /// <summary>
        /// Author of the post
        /// </summary>  
        [FromQuery(Name = "Author")]
        public string? User { get; set; }

        /// <summary>
        /// Filter on this Tag
        /// </summary>  
        public string? Tag { get; set; }

        /// <summary>
        /// Example: "CommentsCount desc" or "user"
        /// </summary>
        public string? OrderBy { get; set; }
        
        /// <summary>
        /// Page to return (starting at 1)
        /// </summary>
        public int? PageNo { get; set; }
        
        /// <summary>
        /// Number of records per page
        /// </summary>
        public int? PageSize { get; set; }
    }
}
