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
	}
}