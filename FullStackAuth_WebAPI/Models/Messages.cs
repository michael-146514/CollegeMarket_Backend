using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)] 
        public string Content { get; set; }

        [Required]
        public int ConversationId { get; set; } 

        [ForeignKey("ConversationId")]
        public Conversation Conversation { get; set; } 

        [Required]
        public string SenderUserId { get; set; } 

        [ForeignKey("SenderUserId")]
        public User SenderUser { get; set; } 

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Timestamp { get; set; } 
    }
}
