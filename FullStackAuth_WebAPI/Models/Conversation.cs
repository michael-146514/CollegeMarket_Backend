using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Title { get; set; }

        public ICollection<UserConversation> UserConversations { get; set; } = new List<UserConversation>();
   
        public ICollection<Messages> Messages { get; set; } = new List<Messages>();

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } 
    }

    public class UserConversation
    {
        [Key]
        public int Id { get; set; }

        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
