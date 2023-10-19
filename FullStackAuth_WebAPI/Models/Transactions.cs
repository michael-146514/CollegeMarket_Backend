using Google.Protobuf.WellKnownTypes;
using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.Models
{
    public class Transactions
    {
        [Key]
        public int Id { get; set; }
        public Timestamp TimeStamp { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string UserID { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int IPAddress { get; set; }
    }
}
