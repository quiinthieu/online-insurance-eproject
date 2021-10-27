using System;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialController : Controller
    {
        // Declare a variable to hold the CredentialService object
        private ICredentialService _credentialService;

        // Constructor --> Initialize the above variable with a CredentialService object 
        public CredentialController(ICredentialService credentialService)
        {
            _credentialService = credentialService;
        }

        // Register a new account
        [HttpPost("register")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Register([FromBody] Credential credential)
        {
            try
            {
                return Ok(_credentialService.Create(credential));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // Login
        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Login(string email, string password)
        {

            dynamic credential = _credentialService.FindByEmailAndPassword(email, password);
            if (credential == null)
            {
                throw new UnauthorizedAccessException("Email and/or password is incorrect.");
            }
            else if (!credential.Status)
            {

            }
            return Ok(_credentialService.Create(credential));

        }


        // Logout
        [HttpGet("logout")]
        [Consumes("application/json")]
        public IActionResult Logout()
        {

            HttpContext.SignOutAsync();
         /*   HttpContext.Items.Clear();*/
            return Ok();

        }


    }
}