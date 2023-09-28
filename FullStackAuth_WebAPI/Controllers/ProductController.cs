using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET api/<ProductController>/5
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            try
            {
                var product = _context.Products.Find(Id);

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

        // POST api/<ProductController>
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Product data)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                data.UserId = userId;

                _context.Products.Add(data);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                
                return StatusCode(201, data);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{Id}"), Authorize]
        public IActionResult Put(int Id, [FromBody] Product data)
        {
            try
            {
            var existingProduct = _context.Products.Find(Id);

                 if(existingProduct == null)
                 {
                    return NotFound();
                 }

                var userId = User.FindFirstValue("id");


                if (userId == existingProduct.UserId || User.IsInRole("Admin"))
                {
                existingProduct.Title = data.Title;
                existingProduct.Price = data.Price;
                existingProduct.Description = data.Description;
                existingProduct.Category = data.Category;
                existingProduct.Status = data.Status;
                existingProduct.IsActive = data.IsActive;

                _context.SaveChanges();
                return Ok(existingProduct);
                }
                    return Unauthorized();


            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.Find(id);

                if (product == null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue("id");


                if (userId == product.UserId || User.IsInRole("Admin"))
                {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok();
                }
                    return Unauthorized();


            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
