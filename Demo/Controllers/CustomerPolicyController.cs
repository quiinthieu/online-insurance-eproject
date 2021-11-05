﻿using System;
using Demo.Helpers;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("customer-policy")]
	public class CustomerPolicyController : Controller
	{
		private ICustomerPolicyService _customerPolicyService;
		private IPolicyService _policyService;

		public CustomerPolicyController(ICustomerPolicyService customerPolicyService, IPolicyService policyService)
		{
			_customerPolicyService = customerPolicyService;
			_policyService = policyService;
		}
		
		[HttpGet("all-customerPolicies")]
		[Produces("application/json")]
		public IActionResult FindAll ()
		{
			try
			{
				return Ok(_customerPolicyService.FindAll());
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("customerPolicy-details/{id}")]
		[Produces("application/json")]
		public IActionResult FindById(int id)
		{
			try
			{
				return Ok(_customerPolicyService.FindById(id));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}

		[HttpPost("create-customerPolicy")]
		[Produces("application/json")]
		[Consumes("application/json")]
		public IActionResult Create([FromBody] CustomerPolicy customerPolicy)
		{
			try
			{
				// Lay ve term tu policy thong qua service vi doi tuong customerPolicy co Policy null
				int id = customerPolicy.PolicyId;
				dynamic policy = _policyService.FindById(id);
				int? term = policy.Term;

				_customerPolicyService.Create(customerPolicy);
				var pts = PremiumHelper.PremiumTransactionScheludler(term, customerPolicy);
				return Ok(pts);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
	}
}