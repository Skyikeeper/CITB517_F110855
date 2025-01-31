using Microsoft.AspNetCore.Mvc;
using TerryPratchettSite.DTOs;
using TerryPratchettSite.Interfaces;
using TerryPratchettSite.Services;

namespace TerryPratchettSite.Controllers
{

    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this._reviewService = reviewService;
        }

        [HttpGet]
        [Route("Reviews/Index")]
        public async Task<IActionResult> Index()
        {
            var viewModel = new ReviewViewModel
            {
                Reviews = await _reviewService.GetReviewsAsync(),
                ReviewForm = new ReviewDTO
                {
                    Content = string.Empty // Initialize the required Content property
                }
            };
            return View(viewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(review);
        }



        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Create(ReviewDTO reviewDTO)
        {
            // Automatically set the Email if it's not already populated (e.g., from the logged-in user)
            if (string.IsNullOrEmpty(reviewDTO.Email))
            {
                reviewDTO.Email = User.Identity.Name;  // This assumes the logged-in user's email is in User.Identity.Name
            }


            if (ModelState.IsValid)
            {
                // If the model is valid, call the service to create the review
                await _reviewService.CreateReviewsAsync(reviewDTO, HttpContext.User);

                // Redirect to the Index or another success view after creating the review
                return RedirectToAction("Index");
            }

            // If model validation fails, return the same view with validation errors
            return View("Create", reviewDTO);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReviewDTO reviewDTO)
        {
            if (ModelState.IsValid)
            {
                {
                    // Call the method to update the review, passing the current user
                    await _reviewService.UpdateReviewAsync(id, reviewDTO);
                    return RedirectToAction("Index");  // Redirect after a successful update
                }
            }
            return this.View(); // Return to the view with the model if validation fails
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return this.View();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteReview(int id)
        {

            try
            {
                // Call the method to update the review, passing the current user
                await _reviewService.DeleteReviewAsync(id);
                return RedirectToAction("Index");  // Redirect after a successful update
            }
            catch (UnauthorizedAccessException)
            {
                // Handle unauthorized access, e.g., redirect to a forbidden page or show a message
                return Forbid();  // Or redirect to an error page
            }
            
        }
    }
}
