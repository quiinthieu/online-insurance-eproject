using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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

	
		[HttpGet("all-roles")]
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
		
		[HttpGet("role-details/{id}")]
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


		[HttpGet("test1")]
		[Produces("application/json")]
		public IActionResult test1()
		{
				return Ok("test1");
		}

		[Authorize(Roles =("AGENT"))]
		[HttpGet("test2")]
		[Produces("application/json")]
		public IActionResult test2()
		{
			return Ok("test2");
		}

		[Authorize(Roles = ("USER"))]
		[HttpGet("test3")]
		[Produces("application/json")]
		public IActionResult test3()
		{
			return Ok("test3");
		}

		[Authorize]
		[HttpGet("credential")]
		[Produces("application/json")]
		public IActionResult getCredential()
		{
			return Ok(HttpContext.Items["credential"]);
		}

		[HttpGet("credential1")]
		[Produces("application/json")]
		public IActionResult getCredential1()
		{
			return Ok(HttpContext.Items["credential"]);
		}
		
		[HttpGet("count")]
		[Produces("application/json")]
		public IActionResult Count()
		{
			try
			{
				return Ok(new
				{
					Result = _roleService.Count()
				});
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}


	}
}