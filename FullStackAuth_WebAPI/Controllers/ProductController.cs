using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }




        // GET api/<ProductController>/5
        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
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

        [HttpGet("user/{userId}")]
        public IActionResult GetYour(string userId)
        {
            try
            {
                var product = _context.Products.Where(p => p.UserId == userId).Include(p => p.ImageUrls);

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
        public async Task<IActionResult> Post([FromForm] ProductFormData formData)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var product = new Product
                {
                    Title = formData.Title,
                    Description = formData.Description,
                    Price = formData.Price,
                    Condition = formData.Condition,
                    Category = formData.Category,
                    Zipcode = formData.Zipcode,
                    IsActive = formData.IsActive, // Assuming IsActive is a property of ProductFormData
                    Status = formData.Status, // Assuming Status is a property of ProductFormData
                    UserId = userId,
                    ImageUrls = new List<ImageUrl>(),
                };

                // Handle image uploads and save their URLs
                foreach (var imageFile in formData.Images)
                {
                    string imageUrl = await SaveImage(imageFile); // Implement the SaveImage method to save the image and return its URL
                    var imageUrlEntity = new ImageUrl { Url = imageUrl, ProductID = product.Id }; // Set the ProductID here
                    product.ImageUrls.Add(imageUrlEntity);
                }

                product.UserId = userId;

                _context.Products.Add(product);
                _context.SaveChanges();

                return StatusCode(201, product);
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


                if (userId == product.UserId )
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


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {


            if (imageFile == null || string.IsNullOrEmpty(imageFile.FileName))
            {

                return "Image file is missing or invalid.";
            }

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(imageFile.FileName);

            if (string.IsNullOrEmpty(fileNameWithoutExtension))
            {

                return "Image file name is missing or invalid.";
            }

            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

    }
}
