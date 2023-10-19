using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/seller")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SellerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        public async Task<IActionResult> CreateStripeAccount()
        {
            string stripeApiKey = _configuration["STRIPE_API_KEY"];


            StripeConfiguration.ApiKey = stripeApiKey; 

            var options = new AccountCreateOptions
            {
                Country = "US",
                Type = "express",
                BusinessType = "individual",
            };

            try
            {
                var service = new AccountService();
                var account = await service.CreateAsync(options);
             

               

                // Handle the newly created Stripe account as needed
                // You can return a response or redirect to another page.
                return Ok(account);
            }
            catch (StripeException ex)
            {
                // Handle any errors that may occur during the Stripe account creation
                // You can return an error response or redirect to an error page.
                return BadRequest(ex.Message);
            }
        }

        // POST api/<SellerController>
        [HttpPost("signup"), Authorize]
        public async Task<IActionResult> RedirectUser()
        {
            StripeConfiguration.ApiKey = "sk_test_51O0ApWF53t9a4tBH435cqCI3N41nIPeRPi55Pah9DKyMSERhMlDWO0kXZNYLeeh8ZAaJpaESMDezNAgmXi7RCOBC00eN1X4BX2"; // Replace with your Stripe secret key

            var options = new AccountLinkCreateOptions
            {
                Account = "acct_1O2KYnFbOQv7IIDx", // Replace with the Stripe account ID you want to link
                RefreshUrl = "http://localhost:3000/refresh", // URL to redirect the user to after they complete the Stripe Connect flow
                ReturnUrl = "http://localhost:3000/success", // URL to redirect the user to upon successful Stripe Connect completion
                Type = "account_onboarding",
            };

            try
            {
                var service = new AccountLinkService();
                var accountLink = await service.CreateAsync(options);

                // Get the URL to redirect the user for Stripe Connect
                string redirectUrl = accountLink.Url;

                // You can return the redirect URL to your frontend for the user to click and complete the Stripe Connect flow.
                return Ok(new { redirectUrl });
            }
            catch (StripeException ex)
            {
                // Handle any errors that may occur during the Stripe account link creation
                // You can return an error response or redirect to an error page.
                return BadRequest(ex.Message);
            }
        }
    }
}
