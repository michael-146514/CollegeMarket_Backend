using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.Models
{
    public class AccountBalances
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public double Balance { get; set; }
    }
}
