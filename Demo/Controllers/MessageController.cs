using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MessageController : Controller
	{
		private IMessageService _messageService;

		public MessageController(IMessageService messageService)
		{
			_messageService = messageService;
		}
		
		[HttpGet("all-messages")]
		[Produces("application/json")]
		public IActionResult FindAll()
		{
			try
			{
				return Ok(_messageService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("message-details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_messageService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("messages/{status}")]
		[Produces("application/json")]
		public IActionResult FindById(bool status)
		{
			try
			{
				return Ok(_messageService.FindByStatus(status));
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
					Result = _messageService.Count()
				});
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}