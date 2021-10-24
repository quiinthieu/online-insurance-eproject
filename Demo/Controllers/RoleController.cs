using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RoleController : ControllerBase
	{
		private IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			this._roleService = roleService;
		}

		[HttpGet("find-all")]
		[Produces("application/json")]
		public IActionResult FindAll()
		{
			try
			{
				return Ok(_roleService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			};
		}
		
		[HttpGet("find-by-id/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_roleService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			};
		}
	}
}