using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("user")]
        public IActionResult SearchUser(string query)
        {
            try
            {

                var Users = _context.Users
                .Where(u => (u.Id.Contains(query) || u.FirstName.Contains(query)) || u.LastName.Contains(query) || u.UserName.Contains(query) || u.Email.Contains(query))
               .ToList();

                if (Users.Count == 0)
                {
                    return NotFound();
                }

                return Ok(Users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("listings")]
        public IActionResult GetListings(string query
            )
        {
            try
            {

                var matchingProducts = _context.Products
                .Where(p => (p.Title.Contains(query) || p.Description.Contains(query)) || p.Category.Contains(query) || p.UserId.Contains(query)).Include(p => p.ImageUrls)
               .ToList();


                
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


        // GET api/<ProductController>/5
        [HttpGet("product/{Id}")]
        public IActionResult GetProduct(int Id)
        {
            try
            {
                var product = _context.Products
                    .Include(p => p.ImageUrls) // Include the related ImageUrls
                    .SingleOrDefault(p => p.Id == Id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        // PUT api/<ProductController>/5
        [HttpPut("product/disable/{Id}"), Authorize]
        public IActionResult SetProductActiveFalse(int Id, [FromBody] Product data)
        {
            try
            {
                var existingProduct = _context.Products.Find(Id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

          
                    existingProduct.Title = existingProduct.Title;
                    existingProduct.Price = existingProduct.Price;
                    existingProduct.Description = existingProduct.Description;
                    existingProduct.Category = existingProduct.Category;
                    existingProduct.Status = existingProduct.Status;
                    existingProduct.IsActive = false;

                    _context.SaveChanges();
                    return Ok(existingProduct);
               
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("product/{id}"), Authorize]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product == null)
                {
                    return NotFound();
                }

                
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    return Ok();
              
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }


        // GET api/<ProductController>/5
        [HttpGet("user/{Id}")]
        public IActionResult GetUser(string Id)
        {
            try
            {
                var User = _context.Users.Where(u => u.Id == Id);

                if (User == null)
                {
                    return NotFound();
                }

                return Ok(User);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("disable/user/{Id}")]
        public IActionResult DisableUser(string Id)
        {
            try
            {
                var user = _context.Users.Find(Id);

                if (user == null)
                {
                    return NotFound();
                }

                user.IsActive = false;

                _context.SaveChanges();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<AdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
