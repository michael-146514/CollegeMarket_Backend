using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.Models
{
    
    public class Conversation
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserOne { get; set; }

        public string UserTwo { get; set; }

        public ICollection<Messages> Messages { get; set; } = new List<Messages>();
    }

    public class CreateConversationRequest
    {
        public string Title { get; set; }
        public List<UserDto> UserIds { get; set; }
    }

    public class UserDto
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
    }

}
