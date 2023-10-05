using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/messages")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {

        //private readonly ApplicationDbContext _context;

        //public MessageController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/messages
        //[HttpGet]
        //public IActionResult GetMessages()
        //{
        //    try
        //    {
        //        var messages = _context.Messages.ToList();
        //        return Ok(messages);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //// GET: api/messages/{id}
        //[HttpGet("{id}")]
        //public IActionResult GetMessage(int id)
        //{
        //    try
        //    {
        //        var message = _context.Messages.Find(id);

        //        if (message == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //// POST: api/messages
        //[HttpPost]
        //public IActionResult CreateMessage([FromBody] Messages message)
        //{
        //    try
        //    {
        //        message.Timestamp = DateTime.UtcNow;

        //        _context.Messages.Add(message);
        //        _context.SaveChanges();

        //        return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //// PUT: api/messages/{id}
        //[HttpPut("{id}")]
        //public IActionResult UpdateMessage(int id, [FromBody] Messages message)
        //{
        //    if (id != message.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(message).State = EntityState.Modified;

        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MessageExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/messages/{id}
        //[HttpDelete("{id}")]
        //public IActionResult DeleteMessage(int id)
        //{
        //    var message = _context.Messages.Find(id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Messages.Remove(message);
        //    _context.SaveChanges();

        //    return NoContent();
        //}

        //private bool MessageExists(int id)
        //{
        //    return _context.Messages.Any(e => e.Id == id);
        //}
    }
}
