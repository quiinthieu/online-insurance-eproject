using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AgentController : Controller
	{
		private IAgentService _agentService;

		public AgentController(IAgentService agentService)
		{
			_agentService = agentService;
		}
		
		[HttpGet("all-agents")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_agentService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("agent-details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_agentService.FindById(id));
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
					Result = _agentService.Count()
				});
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}