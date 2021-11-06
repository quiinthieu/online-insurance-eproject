using System;
using System.Linq;
using Demo.Helpers;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
	[ApiController]
	[Route("customer-policy")]
	public class CustomerPolicyController : Controller
	{
		private ICustomerPolicyService _customerPolicyService;
		private ICustomerService _customerService;
		private IPolicyService _policyService;
		private IPremiumTransactionService _premiumTransactService;


		public CustomerPolicyController(ICustomerPolicyService customerPolicyService, IPolicyService policyService, ICustomerService customerService, IPremiumTransactionService premiumTransactService)
		{
			_customerPolicyService = customerPolicyService;
			_policyService = policyService;
			_customerService = customerService;
			_premiumTransactService = premiumTransactService;
		}
		
		[HttpGet("all-customer-policies")]
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
		
		[HttpGet("customer-policies/{customerId}")]
		[Produces("application/json")]
		public IActionResult FindByCustomerId (int customerId)
		{
			try
			{
				return Ok(_customerPolicyService.FindByCustomerId(customerId));
			}
			catch (Exception e)
			{
				return BadRequest();
			}
		}
		
		[HttpGet("customer-policy-details/{id}")]
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

		[HttpPost("create-customer-policy")]
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
				// Tinh toan premium amount
				customerPolicy.PremiumAmount = policy.Term * policy.Amount;

				// Gan startdate
				customerPolicy.StartDate = DateTime.Now;

				// Gan enddate
				customerPolicy.EndDate = customerPolicy.StartDate.Value.AddYears((int)term);


				// Gan customer
				dynamic loginAccount = HttpContext.Items["credential"];

				int credenId = loginAccount.Id;

				dynamic customer = _customerService.FindByCredentialId(credenId);

				int customerId = customer.Id;

				customerPolicy.CustomerId = customerId;

				// Them customerpolicy
				_customerPolicyService.Create(customerPolicy);

				var pts = PremiumHelper.PremiumTransactionScheludler(term, customerPolicy);

				_premiumTransactService.Create(pts);

				return Ok(new
				{
					msg="Successfully added"
				});
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
	}
}