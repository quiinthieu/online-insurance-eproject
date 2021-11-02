using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("insurance-type")]
	public class InsuranceTypeController : Controller
	{
		private IInsuranceTypeService _insuranceTypeService;

		public InsuranceTypeController(IInsuranceTypeService insuranceTypeService)
		{
			_insuranceTypeService = insuranceTypeService;
		}
		
		[HttpGet("find-all")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_insuranceTypeService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_insuranceTypeService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}