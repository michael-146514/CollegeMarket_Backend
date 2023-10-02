using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

                // Retrieve conversations where the user is either the sender or receiver
                var conversations = _context.Conversations
            .Where(c => c.UserConversations.Any(u => u.UserId == userId))
            .ToList();

                return Ok(conversations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/conversations/{id}
        [HttpGet("{id}")]
        public IActionResult GetConversation(int id)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                var conversation = _context.Conversations
                    .Include(c => c.Messages) 
                    .Where(c => c.Id == id && c.UserConversations.Any(u => u.UserId == userId))
                    .FirstOrDefault();

                if (conversation == null)
                {
                    return NotFound();
                }

                return Ok(conversation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/conversations
        [HttpPost]
        public IActionResult CreateConversation([FromBody] Conversation conversation)
        {
            try
            {
                _context.Conversations.Add(conversation);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetConversation), new { id = conversation.Id }, conversation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/conversations/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteConversation(int id)
        {
            string userId = User.FindFirstValue("id");

            var conversation = _context.Conversations
                .Include(c => c.UserConversations)
                .Where(c => c.Id == id && c.UserConversations.Any(u => u.UserId == userId))
                .FirstOrDefault();

            if (conversation == null)
            {
                return NotFound();
            }

            _context.Conversations.Remove(conversation);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.Id == id);
        }
    }
}
