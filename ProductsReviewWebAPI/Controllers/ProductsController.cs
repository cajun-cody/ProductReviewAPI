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
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult Get([FromQuery] double? maxPrice)
        {
            //Grab all products then check if a param is queried. If so, logic to narrow the list otherwise return list. 
            var products = _context.Products.ToList();
            if(maxPrice != null)
            {
                products = products.Where(p => p.Price <= maxPrice).ToList();
            }          
            return Ok(products);
            
        }

        // GET api/Products/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //Create a new DTO for product and a new review DTO to display a list of the reviews.After list of reviews average the review rating.  
            var product = _context.Products.Include(r => r.Reviews).Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Reviews = p.Reviews.Select(r => new ReviewDTO
                {
                    Text = r.Text,
                    Rating = r.Rating,
                }).ToList(),
                AverageRating = p.Reviews.Average(r => r.Rating) //Built in average of the reviews. 
            })
                .FirstOrDefault(p => p.Id == id); //We keep the find/firstordefault after logic to prevent circular error. 
            return Ok(product);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201, product);
        }

        // PUT api/Products/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product) //There are a few ways to do this. Check dcc github. 
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Reviews = product.Reviews;
            _context.SaveChanges();
            return Ok(existingProduct);
        }

        // DELETE api/Products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
 

        }
    }
}
