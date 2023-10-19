using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.Models
{
    public class WatchList
    {
        [Key]
        public int Id { get; set; }
        public int productId { get; set; }
        public string userId { get; set; }
        
    }
}
