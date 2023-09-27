using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Messages
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("User")]
        public string UserIdSender { get; set; }

        [ForeignKey("User")]
        public string UserIdReceiver { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }
}
