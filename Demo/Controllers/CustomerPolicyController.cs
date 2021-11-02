using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CustomerPolicyController : Controller
	{
		private ICustomerPolicyService _customerPolicyService;

		public CustomerPolicyController(ICustomerPolicyService customerPolicyService)
		{
			_customerPolicyService = customerPolicyService;
		}
		
		[HttpGet("all-customerPolicies")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_customerPolicyService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("customerPolicy-details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_customerPolicyService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}