using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsReviewWebAPI.Data;
using ProductsReviewWebAPI.DataTransferObjects;
using ProductsReviewWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductsReviewWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public IActionResult Get()
        {
            var reviews = _context.Reviews.ToList();
            return Ok(reviews);
        }

        // GET api/Reviews/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //Find a single review by passing in a review Id. 
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }
        //Get api/Reviews/GetByProductId
        [HttpGet("GetByProductId/{productId}")]
        public IActionResult GetByProductId(int productId)
        {
            //Variable to hold query in the reviews table to return all the review of a product with a particular Id. 
            var reviews = _context.Reviews.Where(r => r.ProductId == productId).ToList();
            return Ok(reviews);
        }


        // POST api/Reviews
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return StatusCode(201, review);
        }

        // PUT api/Reviews/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review review)
        {
            var existingReview = _context.Reviews.Find(id);
            if (existingReview == null)
            {
                return NotFound();
            }

            existingReview.Text = review.Text;
            existingReview.Rating = review.Rating;
            existingReview.ProductId = review.ProductId;
            _context.SaveChanges();
            return Ok(existingReview);
        }

        // DELETE api/Reviews/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return NoContent();
        }

       
    }
}
