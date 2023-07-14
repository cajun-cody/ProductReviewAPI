using System.ComponentModel.DataAnnotations;

namespace ProductsReviewWebAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        //Navigation property
        public ICollection<Review> Reviews { get; set; }
    }
}
