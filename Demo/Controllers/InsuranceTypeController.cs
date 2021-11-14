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
		
		[HttpGet("all-insurance-types")]
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
		
		[HttpGet("insurance-type-details/{id}")]
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
		
		[HttpGet("count")]
		[Produces("application/json")]
		public IActionResult Count()
		{
			try
			{
				return Ok(new
				{
					Result = _insuranceTypeService.Count()
				});
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
	}
}