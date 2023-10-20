using System.Security.Claims;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/watchlist")]
    [ApiController]
    
    public class WatchListController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public WatchListController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        // GET api/<WatchList>/5
        [HttpGet("user/{userId}"), Authorize]
        public IActionResult GetUserWatchList(string userId)
        {
            
            var watchListItems = _context.WatchList
                .Where(w => w.userId == userId)
                .ToList();

            if (watchListItems == null || watchListItems.Count == 0)
            {
                return NotFound(); 
            }

            return Ok(watchListItems);
        }

        [HttpGet("{id}"), Authorize]
        public IActionResult GetWatchListID(int id){
            try{
string userId = User.FindFirstValue("id");

                var watchlistItem = _context.WatchList.Where(w => w.productId == id && w.userId == userId).ToList();

                return Ok(watchlistItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
                  
        }

        // POST api/<WatchList>
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] WatchList watchList)
        {
            if (watchList == null)
            {
                return BadRequest("WatchList data is invalid.");
            }

            _context.WatchList.Add(watchList);
            _context.SaveChanges();

            // Return a 201 Created response with the newly created WatchList and its location.
            return CreatedAtRoute("GetWatchList", new { id = watchList.Id }, watchList);
        }

        // DELETE api/<WatchList>/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            var watchList = _context.WatchList.Find(id);

            if (watchList == null)
            {
                return NotFound(); 
            }

            _context.WatchList.Remove(watchList);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
