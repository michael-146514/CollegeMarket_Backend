using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projectName.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        // Declare ApplicationDbContext to interact with the database and IWebHostEnvironment to get the root path of the project.
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        // Inject ApplicationDbContext and IWebHostEnvironment through the constructor
        public ImageController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }


        // GET: api/Image
        // This action method gets all the images from the database.
        [HttpGet("{imageName}.{format}")]
        public async Task<ActionResult<IEnumerable<Image>>> GetImagesByName(string imageName, string format)
        {
            try
            {
                if (string.IsNullOrEmpty(imageName) || string.IsNullOrEmpty(format))
                {
                    return BadRequest("Image name or format cannot be empty.");
                }

                // Check if the format is a valid image format (e.g., png, jpeg, etc.). You can add more valid formats as needed.
                string[] validFormats = { "png", "jpeg", "jpg", "gif" };
                if (!validFormats.Contains(format.ToLower()))
                {
                    return BadRequest("Invalid image format.");
                }

                // Combine the image name and format to form the complete file name.
                string completeImageName = $"{imageName}.{format}";

                // Check if the image file with the complete name exists.
                string imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", completeImageName);
                if (!System.IO.File.Exists(imagePath))
                {
                    return NotFound("Image not found.");
                }

                // Create an Image object for the found image.
                var image = new Image
                {
                    Title = completeImageName,
                    ImageSrc = string.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, completeImageName)
                };

                return Ok(image);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        // POST: api/Images
        // This action method receives an Image object from a POST request.
        [HttpPost]
        public async Task<ActionResult<Image>> PostNewImage([FromForm] Image value)
        {
            // Save the image from the Image object to the Images folder.
            // The SaveImage method returns the name of the image, which is then assigned to the Title property.
            value.Title = await SaveImage(value.ImageFile);

            // Add the Image object to the Image table in the database and save changes.
            _context.Image.Add(value);
            _context.SaveChanges();

            // Return a 201 Created status code and the Image object.
            return StatusCode(201, value);
        }

        // This method saves an image file to the Images folder and returns the name of the image.
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
