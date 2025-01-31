using System.ComponentModel.DataAnnotations;

namespace TerryPratchettSite.Models
{
    public class Reviews : BaseEntity
    {
        public string Email { get; set; }

        [Required]
        [MaxLength(1000)] // Limit review content length
        public required string Content { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public TPUser? User { get; set; }

        // Constructor to set default values
        public Reviews()
        {
            Date = DateTime.Now; // Default to current time
        }
    }
}
