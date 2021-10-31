using System;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("")]
	public class DemoController : Controller
	{
		[HttpGet("~/")]
		[Produces("application/json")]
		public IActionResult Index()
		{
			try
			{
				return Ok($"BE is running on local port {HttpContext.Connection.LocalPort}");
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}