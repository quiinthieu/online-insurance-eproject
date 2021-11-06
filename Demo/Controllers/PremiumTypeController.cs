using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("premium-type")]
	public class PremiumTypeController : Controller
	{
		private IPremiumTypeService _premiumType;

		public PremiumTypeController(IPremiumTypeService premiumType)
		{
			_premiumType = premiumType;
		}
		
		[HttpGet("all-premium-types")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_premiumType.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("premium-type-details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_premiumType.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}