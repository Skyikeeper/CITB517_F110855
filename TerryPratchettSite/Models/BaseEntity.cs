using System.ComponentModel.DataAnnotations;

namespace TerryPratchettSite.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
