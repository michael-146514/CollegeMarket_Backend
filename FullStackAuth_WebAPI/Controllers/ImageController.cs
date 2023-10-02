﻿using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetAllImages()
        {
            // Query the Image table and project the result into a new collection of Image.
            // For each image, the ImageSrc is created by combining the request scheme (http or https),
            // the host, the base path of the request, and the name of the image.
            return await _context.Image.Select(x => new Image()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.Title)
            }).ToListAsync();
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
