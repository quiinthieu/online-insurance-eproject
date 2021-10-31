using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BranchController : Controller
	{
		private IBranchService _branchService;

		public BranchController(IBranchService branchService)
		{
			_branchService = branchService;
		}
		
		[HttpGet("all-branches")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_branchService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("branch-details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_branchService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}