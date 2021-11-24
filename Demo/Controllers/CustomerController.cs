using System;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController : Controller
	{
		private ICustomerService _customerService;
		private IConfiguration _configuration;
		public CustomerController(ICustomerService customerService, IConfiguration configuration)
		{
			_customerService = customerService;
			_configuration = configuration;
		}
		
		[HttpGet("all-customers")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_customerService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("customer-details-by-id/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_customerService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}

		//[Authorize]
		[HttpGet("customer-details-by-credential-id/{id}")]
		[Produces("application/json")]
		public IActionResult FindByCredentialId(int id)
		{
			try
			{
				return Ok(_customerService.FindByCredentialId(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}

		[HttpPut("customer-update/{id}")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public IActionResult Update(int id, [FromBody] Customer customer)
        {
            try
            {
				return Ok(_customerService.Update(id, customer));
            }
            catch (Exception ex)
            {
				return BadRequest();
            }
        }
		
		[HttpGet("count")]
		[Produces("application/json")]
		public IActionResult Count()
		{
			try
			{
				return Ok(new
				{
					Result = _customerService.Count()
				});
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}

		[HttpPost("send-contact")]
		[Produces("application/json")]
		[Consumes("application/json")]
		public IActionResult SendContact([FromBody] Contact contactForm)
        {
            try
            {
				var mailHelper = new Helpers.MailHelper(_configuration);
				var subject = "More information about online insurance";
				var body = "Hi " + contactForm.FirstName + " " + contactForm.LastName + ". We appreciate your cooperation. Click the link down below for details !";
				var attachment = "https://www.hdfcergo.com/blogs/general-insurance/5-amazing-benefits-of-online-insurance";
				mailHelper.Send(_configuration["Gmail:Username"], contactForm.Email, subject, body, attachment);
				return Ok(new
				{
					msg = "Success"
				});
            }
            catch (Exception ex)
            {
				return null;
            }
        }
	}
}