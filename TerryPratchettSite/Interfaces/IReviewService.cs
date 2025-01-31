using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TerryPratchettSite.DTOs;
using TerryPratchettSite.Models;

namespace TerryPratchettSite.Interfaces
{
    public interface IReviewService
    {
        Task CreateReviewsAsync( ReviewDTO reviewDTO, ClaimsPrincipal claimsPrincipal);
        Task UpdateReviewAsync(int id, ReviewDTO reviewDTO);
        Task DeleteReviewAsync(int id);
        Task<IEnumerable<Reviews>> GetReviewsAsync();
        Task<Reviews?> GetReviewByIdAsync(int id);
    }
}
