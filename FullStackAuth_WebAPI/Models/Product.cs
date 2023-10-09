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

        [Required]
        public string Condition { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int Zipcode { get; set; }

        // Define a list to store image URLs
        public ICollection<ImageUrl> ImageUrls { get; set; }

        public bool IsActive { get; set; }

        public string Status { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }

    public class ProductFormData
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public int Zipcode { get; set; }

        // Define a list to accept multiple image files
        public List<IFormFile> Images { get; set; }

        public bool IsActive { get; set; }

        public string Status { get; set; }
   
    }

    public class ImageUrl
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int ProductID { get; set; }

    }
}
