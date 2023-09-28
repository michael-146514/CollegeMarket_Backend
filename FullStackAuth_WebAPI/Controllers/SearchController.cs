using FullStackAuth_WebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }




        [HttpGet]
        public IActionResult GetBySearch(string query, int zipcode)
        {
            try
            {
                
                var matchingProducts = _context.Products
                .Where(p => (p.Title.Contains(query) || p.Description.Contains(query)) || p.Category.Contains(query) && p.Zipcode == zipcode)
               .ToList();


                if (matchingProducts.Count == 0)
                {
                    matchingProducts = _context.Products
                 .Where(p => p.Title.Contains(query) || p.Description.Contains(query) || p.Category.Contains(query))
                    .ToList();
                }

                if (matchingProducts.Count == 0)
                {
                    return NotFound();
                }

                return Ok(matchingProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // GET api/<SearchController>/5
        [HttpGet("category/{category}")]
        public IActionResult GetByCategory(string category)
        {
            try
            {
                // Perform a query to filter products by category
                var productsInCategory = _context.Products
                    .Where(p => p.Category == category)
                    .ToList();

                if (productsInCategory.Count == 0)
                {
                    return NotFound();
                }

                return Ok(productsInCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // POST api/<SearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
