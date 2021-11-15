using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("premium-type")]
	public class PremiumTypeController : Controller
	{
		private IPremiumTypeService _premiumTypeService;

		public PremiumTypeController(IPremiumTypeService premiumTypeService)
		{
			_premiumTypeService = premiumTypeService;
		}
		
		[HttpGet("all-premium-types")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_premiumTypeService.FindAll());
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
				return Ok(_premiumTypeService.FindById(id));
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
					Result = _premiumTypeService.Count()
				});
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}