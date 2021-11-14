using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ClaimController : Controller
	{
		private IClaimService _claimService;

		public ClaimController(IClaimService claimService)
		{
			_claimService = claimService;
		}
		
		[HttpGet("all-claims")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_claimService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("claim-details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_claimService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("claims-by-customer-policy-id/{customerPolicyId}")]
		[Produces("application/json")]
		public IActionResult FindByCustomerPolicyId(int customerPolicyId)
		{
			try
			{
				return Ok(_claimService.FindByCustomerPolicyId(customerPolicyId));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}

		[HttpGet("claims-by-customer-id/{customerId}")]
		[Produces("application/json")]
		public IActionResult FindByCustomerId(int customerId)
		{
			try
			{
				return Ok(_claimService.FindByCustomerId(customerId));
			}
			catch (Exception e)
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
					Result = _claimService.Count()
				});
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}