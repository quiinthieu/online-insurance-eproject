using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SubscriptionController : Controller
	{
		private ISubscriptionService _subscriptionService;

		public SubscriptionController(ISubscriptionService subscriptionService)
		{
			_subscriptionService = subscriptionService;
		}
		
		[HttpGet("all-subscriptions")]
		[Produces("application/json")]
		public IActionResult FindAll()
		{
			try
			{
				return Ok(_subscriptionService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpPost("unsubscribe")]
		[Produces("application/json")]
		[Consumes("application/json")]
		public IActionResult Unsubscribe(string email)
		{
			try
			{
				return Ok(new
				{
					Result = _subscriptionService.Unsubscribe(email)
				});
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
					Result = _subscriptionService.Count()
				});
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}