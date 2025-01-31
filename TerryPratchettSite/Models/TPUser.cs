using Microsoft.AspNetCore.Identity;

namespace TerryPratchettSite.Models
{
    public class TPUser : IdentityUser<int>
    {
        public List<Reviews> Reviews { get; set; } = new List<Reviews>();
    }
}
