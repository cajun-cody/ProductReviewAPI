﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsReviewWebAPI.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Rating { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
    }
}