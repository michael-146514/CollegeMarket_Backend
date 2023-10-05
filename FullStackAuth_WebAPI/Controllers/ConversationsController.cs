using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/conversations")]
    [ApiController]
    [Authorize]
    public class ConversationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConversationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetConversations()
        {
            try
            {
                string userId = User.FindFirstValue("id");
                var conversations = _context.Conversations
                    .Where(c => c.UserOne == userId || c.UserTwo == userId)
                    .ToList();

                return Ok(conversations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public IActionResult CreateConversations([FromBody] Conversation data)
        {
            try
            {

                string userId = User.FindFirstValue("id");
                // Create a new conversation using the extracted data
                var newConversation = new Conversation
                {
                    Title = data.Title,
                    UserOne = userId,
                    UserTwo = data.UserTwo
                    // Other conversation properties
                };

                // Add the new conversation to the database using Entity Framework or your data access method
                _context.Conversations.Add(newConversation);
                _context.SaveChanges();

                return Ok(newConversation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Handle any exceptions.
            }
        }


        [HttpGet("{conversationId}/messages")]
        public IActionResult GetConversationsMessages(int conversationId)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                var conversation = _context.Conversations.FirstOrDefault(c => c.Id == conversationId);
                if (conversation == null)
                {
                    return NotFound("Conversation not found.");
                }

                if (conversation.UserOne == userId || conversation.UserTwo == userId)
                {
                    return Unauthorized("You are not authorized to send messages in this conversation.");
                }

                var conversations = _context.Messages
                    .Where(m => m.ConversationId == conversationId)
                    .ToList();

                return Ok(conversations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("{conversationId}/messages")]
        public IActionResult CreateMessage(int conversationId, [FromBody] Messages formData)
        {
            try
            {
                string userId = User.FindFirstValue("id");
                // Check if the conversation exists
                var conversation = _context.Conversations.FirstOrDefault(c => c.Id == conversationId);
                if (conversation == null)
                {
                    return NotFound("Conversation not found.");
                }

                
                // Check if the user's userId is in the UserIds collection of the conversation
                if (conversation.UserOne == userId || conversation.UserTwo == userId)
                {
                    return Unauthorized("You are not authorized to send messages in this conversation.");
                }


                // Create a new message
                var newMessage = new Messages
                {
                    Content = formData.Content,
                    UserId = formData.UserId,
                    ConversationId = conversationId
                };

                // Add the new message to the database
                _context.Messages.Add(newMessage);
                _context.SaveChanges();

                return Ok(newMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // DELETE: api/conversations/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteConversation(int id, [FromQuery] string userId)
        {
            try
            {
                // Retrieve the conversation by its ID
                var conversation = _context.Conversations.FirstOrDefault(c => c.Id == id);

                // Check if the conversation exists
                if (conversation == null)
                {
                    return NotFound();
                }

                // Check if the user's userId is in the UserIds collection of the conversation
                if (conversation.UserOne != userId || conversation.UserTwo != userId)
                {
                    return Unauthorized("You are not authorized to delete this conversation.");
                }

                // If the user is authorized, remove the conversation
                _context.Conversations.Remove(conversation);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
