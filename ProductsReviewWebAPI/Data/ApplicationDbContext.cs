using Microsoft.EntityFrameworkCore;
using ProductsReviewWebAPI.Models;

namespace ProductsReviewWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

    
        }

    }
}
