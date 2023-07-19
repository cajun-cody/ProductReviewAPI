using ProductsReviewWebAPI.Models;

namespace ProductsReviewWebAPI.DataTransferObjects
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
