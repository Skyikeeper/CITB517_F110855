using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TerryPratchettSite.Models;

namespace TerryPratchettSite.Data.Repos
{
    public class UserRepo
    {
        private readonly UserManager<TPUser> userManager;

        public UserRepo(UserManager<TPUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<TPUser> GetUserAsync(ClaimsPrincipal user)
        {
            return await userManager.GetUserAsync(user);
        }


    }
}
