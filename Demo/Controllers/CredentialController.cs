using System;
using System.Diagnostics;
using System.Text;
using Demo.Helpers;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialController : Controller
    {
        // Declare a variable to hold the CredentialService object
        private ICredentialService _credentialService;
        private ICustomerService _customerService;
        private IConfiguration _configuration;


        // Constructor --> Initialize the above variable with a CredentialService object 
        public CredentialController(ICredentialService credentialService, IConfiguration configuration, ICustomerService customerService)
        {
            _credentialService = credentialService;
            _configuration = configuration;
            _customerService = customerService;
        }

        // Register a new account
        [HttpPost("register")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Register([FromBody] Register register)
        {

            Debug.WriteLine(register.Birthday);
            var credential = _credentialService.Create(new Credential
            {
                Email = register.Email,
                Password = register.Password,
            });

            var customer = _customerService.Create(new Customer
            {
                Name = register.Name,
            /*    Birthday = register.Birthday,*/
                Gender = register.Gender,
                Street = register.Street,
                City = register.City,
                State = register.State,
                ZipCode = register.ZipCode,
                Occupation = register.Occupation,
                CitizenId = register.CitizenId,
                CredentialId = credential.Id
            });

            return Ok(credential);
        }


        // Login
        [HttpPost("login")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Login([FromBody] Credential logedinAccount)
        {
            dynamic credential = _credentialService.FindByEmailAndPassword(logedinAccount.Email, logedinAccount.Password);
            if (credential == null)
            {
                throw new UnauthorizedAccessException("Email and/or password is incorrect.");
            }
            else if (!credential.Status)
            {
                throw new UnauthorizedAccessException("Account Inactivated");
            }

            return Ok(
                new
                {
                    accessToken = Base64Helper.Base64Encode($"{logedinAccount.Email}:{logedinAccount.Password}"),
                    credential
                });
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

        // Avtivate request
        [HttpPost("activate-request")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult ActivateRequest([FromBody] Credential logedinAccount)
        {

            Debug.WriteLine(logedinAccount.Email);
            var credential = _credentialService.FindByEmail(logedinAccount.Email);
            if (credential == null)
            {
                throw new UnauthorizedAccessException("Account not exist");
            }
            else if (credential != null && credential.Status)
            {
                throw new UnauthorizedAccessException("Account already activated");
            }
            var UUID = Guid.NewGuid().ToString();
            var url = $"{_configuration["Client:ReturnUrl"]}/activate-account?token={Base64Helper.Base64Encode(logedinAccount.Email)}&activation-code={UUID}&type=activate-account";
            var subject = "Activation Code";
            var body = $"Please click the link below to activate your account: <b>{url}</b>";
            new MailHelper(_configuration).Send(_configuration["Gmail:Username"], credential.Email, subject, body);
            var updatedCredential = _credentialService.Update(new Credential
            {
                Email = credential.Email,
                Password = credential.Password,
                Status = credential.Status,
                RoleId = credential.RoleId,
                ActivationCode = UUID
            });
            return Ok(updatedCredential);
        }

        // Avtivate account
        [HttpPost("activate-account")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult ActivateAccount([FromBody] Credential logedinAccount)
        {
            Debug.WriteLine(logedinAccount.ActivationCode);
            Debug.WriteLine(logedinAccount.Email);
            var credential = _credentialService.FindByActivationCodeAndEmail(logedinAccount.ActivationCode, logedinAccount.Email);
            if (credential == null)
            {
                throw new UnauthorizedAccessException("Activation code not exist");
            }
            else if (credential != null && credential.Status)
            {
                throw new UnauthorizedAccessException("Account already activated");
            }
            var UUID = Guid.NewGuid().ToString();
            var updatedCredential = _credentialService.Update(new Credential
            {
                Email = credential.Email,
                Password = credential.Password,
                Status = true,
                RoleId = credential.RoleId,
                ActivationCode = credential.ActivationCode
            });

            return Ok(updatedCredential);

        }


        // Reset password request
        [HttpPost("reset-password-request")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult ResetPasswordRequest([FromBody] Credential logedinAccount)
        {

            Debug.WriteLine(logedinAccount.Email);
            var credential = _credentialService.FindByEmail(logedinAccount.Email);
            if (credential == null)
            {
                throw new UnauthorizedAccessException("Account not exist");
            }

            var UUID = Guid.NewGuid().ToString();
            var subject = "Reset Password Code";
            var url = $"{_configuration["Client:ReturnUrl"]}/activate-account?token={Base64Helper.Base64Encode(logedinAccount.Email)}&activation-code={UUID}&type=forgot-password";
            var body = $"Please click the link below to reset your password account: <b>{url}</b>";
            new MailHelper(_configuration).Send(_configuration["Gmail:Username"], credential.Email, subject, body);
            var updatedCredential = _credentialService.Update(new Credential
            {
                Email = credential.Email,
                Password = credential.Password,
                Status = credential.Status,
                RoleId = credential.RoleId,
                ActivationCode = UUID
            });
            return Ok(updatedCredential);
        }

        // Reset password
        [HttpPost("reset-password")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult ResetPassword([FromBody] Credential logedinAccount)
        {
            Debug.WriteLine(logedinAccount.ActivationCode);
            Debug.WriteLine(logedinAccount.Email);
            Debug.WriteLine(logedinAccount.Password);
            var credential = _credentialService.FindByActivationCodeAndEmail(logedinAccount.ActivationCode, logedinAccount.Email);
            if (credential == null)
            {
                throw new UnauthorizedAccessException("Activation code not exist");
            }

            var UUID = Guid.NewGuid().ToString();
            var updatedCredential = _credentialService.Update(new Credential
            {
                Email = credential.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(logedinAccount.Password),
                Status = true,
                RoleId = credential.RoleId,
                ActivationCode = UUID
            });

            return Ok(updatedCredential);
        }

        // Verify Activation Code And Email
        [HttpPost("verify")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult VerifyActivationCodeAndEmail([FromBody] Credential logedinAccount)
        {
            Debug.WriteLine(logedinAccount.ActivationCode);
            Debug.WriteLine(logedinAccount.Email);
            var credential = _credentialService.FindByActivationCodeAndEmail(logedinAccount.ActivationCode, logedinAccount.Email);
            if (credential == null)
            {
                throw new UnauthorizedAccessException("Activation code not exist");
            }

            return Ok(credential);
        }
        
        [HttpGet("count")]
        [Produces("application/json")]
        public IActionResult Count()
        {
            try
            {
                return Ok(new
                {
                    Result = _credentialService.Count()
                });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

    }
}