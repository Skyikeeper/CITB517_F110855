using System.ComponentModel.DataAnnotations;
using TerryPratchettSite.Models;

namespace TerryPratchettSite.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; } // ID, populated after saving

        /*[EmailAddress]*/ // Validate email format
        public string? Email { get; set; } // The email of the reviewer

        [Required]
        [MaxLength(1000)] // Limit review content to 1000 characters
        public required string Content { get; set; } // Review content

        public DateTime CreateDate { get; set; } = DateTime.Now; // Creation timestamp

        public int UserId { get; set; } // ID of the user associated with the review
    }
}
