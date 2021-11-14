using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("policy")]
	public class PolicyController : Controller
	{
		private IPolicyService _policyService;

		public PolicyController(IPolicyService policyService)
		{
			_policyService = policyService;
		}
		
		[HttpGet("all-policies")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_policyService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("policy-details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_policyService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}

		[HttpGet("policy-by-insurancetype/{id}")]
		[Produces("application/json")]
		public IActionResult FindByInsuranceType(int id)
		{
			try
			{
				return Ok(_policyService.FindByInsuranceTypeId(id));
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
					Result = _policyService.Count()
				});
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}