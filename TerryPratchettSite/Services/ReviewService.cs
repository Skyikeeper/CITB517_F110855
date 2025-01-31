using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Security.Claims;
using TerryPratchettSite.Data;
using TerryPratchettSite.Data.Repos;
using TerryPratchettSite.DTOs;
using TerryPratchettSite.Interfaces;
using TerryPratchettSite.Models;

namespace TerryPratchettSite.Services
{
    public class ReviewService : IReviewService
    {

        private readonly DataContext _dbContext;
        private UserRepo userRepo;
        /// Creates a new review.
        public ReviewService(DataContext dbContext, UserRepo userRepo)
        {
            this._dbContext = dbContext;
            this.userRepo = userRepo;
        }

        public async Task CreateReviewsAsync(ReviewDTO reviewDTO, ClaimsPrincipal claimsPrincipal)
        {
            // Retrieve email from claims
            var email = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (email == null)
            {
                throw new InvalidOperationException("User email not found in claims.");
            }

            // Assign email to the DTO
            reviewDTO.Email = email;

            // Proceed with the rest of the logic
            var user = await userRepo.GetUserAsync(claimsPrincipal);
            reviewDTO.UserId = user.Id;

            var review = new Reviews
            {
                Email = reviewDTO.Email,
                Content = reviewDTO.Content,
                UserId = reviewDTO.UserId,
            };

            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();

            reviewDTO.Id = review.Id;
        }

        /// Deletes a review by its ID.
        public async Task DeleteReviewAsync(int id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);

           

            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
        }
        /// Retrieves a review by its ID.
        public async Task<Reviews?> GetReviewByIdAsync(int id)
        {
            return await _dbContext.Reviews.FindAsync(id);
        }
        /// Retrieves a list of reviews, ordered by date in descending order, limited to 12 r
        public async Task<IEnumerable<Reviews>> GetReviewsAsync()
        {
            return await _dbContext.Reviews.OrderByDescending(r => r.Date).Take(12).ToListAsync();
        }
        /// Updates an existing review.
        public async Task UpdateReviewAsync(int id, ReviewDTO reviewDTO)
        {
            var review = await _dbContext.Reviews.FindAsync(id);

            if (review == null)
            {
                throw new Exception("Review not found.");
            }

            // Update the review content
            review.Content = reviewDTO.Content;

            await _dbContext.SaveChangesAsync();
        }

    }
}
