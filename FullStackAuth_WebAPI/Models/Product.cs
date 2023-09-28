using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
     
        public string Condition { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int Zipcode { get; set; }
        [Required]
        public string ImageUrl_one { get; set; }
        public string ImageUrl_Two { get; set; }
        public string ImageUrl_Three { get; set; }
        public string ImageUrl_Four { get; set; }

        public bool IsActive { get; set; }
        public string Status { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
