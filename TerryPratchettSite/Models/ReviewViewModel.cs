using TerryPratchettSite.DTOs;
using TerryPratchettSite.Models;

public class ReviewViewModel
{
    public IEnumerable<Reviews> Reviews { get; set; }
    public ReviewDTO ReviewForm { get; set; }
}
