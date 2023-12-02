using AutoMapper;
using FullStackAuth_WebAPI.ActionFilters;
using FullStackAuth_WebAPI.Contracts;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YourNamespace.Services;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthenticationManager _authManager;
        private readonly EmailService _emailService;
        public AuthenticationController(
           IMapper mapper,
           UserManager<User> userManager,
           IAuthenticationManager authManager,
           EmailService emailService) // Inject EmailService
        {
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _emailService = emailService;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRoleAsync(user, "USER");

            UserForDisplayDto createdUser = new UserForDisplayDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            // Send welcome email to the newly registered user
            string subject = "Welcome to our platform!";
            string body = $"Hello {createdUser.UserName},\n\nWelcome to our platform!";

            _emailService.SendEmail("your_email@gmail.com", "your_password", createdUser.UserName, subject, body);

            return StatusCode(201, createdUser);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                return Unauthorized();
            }

            return Ok(new { access = await _authManager.CreateToken() });
        }
    }
}