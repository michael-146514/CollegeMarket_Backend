using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FullStackAuth_WebAPI.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }

        [JsonIgnore]
        public Conversation Conversation { get; set; }


    }
}
