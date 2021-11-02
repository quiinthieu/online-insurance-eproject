using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PremiumTransactionController : Controller
	{
		private IPremiumTransactionService _premiumTransactionService;

		public PremiumTransactionController(IPremiumTransactionService premiumTransactionService)
		{
			_premiumTransactionService = premiumTransactionService;
		}
		
		[HttpGet("all-premiumTransactions")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_premiumTransactionService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("premiumTransaction-details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_premiumTransactionService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}