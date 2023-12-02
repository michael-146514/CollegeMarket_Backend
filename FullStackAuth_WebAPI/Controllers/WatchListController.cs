using System.Security.Claims;
using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
  public class WatchListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WatchListController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/watchlist/user/{userId}
        [HttpGet("user/{userId}")]
        public IActionResult GetUserWatchList(string userId)
        {
            // Ensure the user is authorized to access their own watchlist.
            if (User.FindFirstValue("id") != userId)
            {
                return Forbid(); // User is not authorized to access this resource.
            }

            var watchListItems = _context.WatchList
                .Where(w => w.UserId == userId)
                .ToList();

            if (watchListItems == null || watchListItems.Count == 0)
            {
                return NotFound();
            }

            return Ok(watchListItems);
        }

        // GET api/watchlist/{id}
        [HttpGet("{id}")]
        public IActionResult GetWatchListID(int id)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                var watchlistItem = _context.WatchList
                    .Where(w => w.ProductId == id && w.UserId == userId)
                    .SingleOrDefault();

                if (watchlistItem == null)
                {
                    return NotFound();
                }

                return Ok(watchlistItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/watchlist
        [HttpPost]
        public IActionResult Post([FromBody] WatchList watchList)
        {
            if (watchList == null)
            {
                return BadRequest("WatchList data is invalid.");
            }

            watchList.UserId = User.FindFirstValue("id"); // Set the user ID from the token

            _context.WatchList.Add(watchList);
            _context.SaveChanges();

            // Return a 201 Created response with the newly created WatchList and its location.
            return CreatedAtAction("GetWatchListID", new { id = watchList.Id }, watchList);
        }

        // DELETE api/watchlist/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string userId = User.FindFirstValue("id");
            var watchList = _context.WatchList
                .FirstOrDefault(w => w.Id == id && w.UserId == userId);

            if (watchList == null)
            {
                return NotFound();
            }

            _context.WatchList.Remove(watchList);
            _context.SaveChanges();

            return NoContent();
        }
    }

