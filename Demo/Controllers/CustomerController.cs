using System;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerController : Controller
	{
		private ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
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
	}
}