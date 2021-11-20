using System;
using Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [ApiController]
    [Route("premium-transaction")]
    public class PremiumTransactionController : Controller
    {
        private IPremiumTransactionService _premiumTransactionService;

        public PremiumTransactionController(IPremiumTransactionService premiumTransactionService)
        {
            _premiumTransactionService = premiumTransactionService;
        }

        [HttpGet("all-premium-transactions")]
        [Produces("application/json")]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(_premiumTransactionService.FindAll());
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("premium-transaction-details/{id}")]
        [Produces("application/json")]
        public IActionResult FindById(int id)
        {
            try
            {
                return Ok(_premiumTransactionService.FindById(id));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("premium-transactions/{customerPolicyId}")]
        [Produces("application/json")]
        public IActionResult FindByCustomerPolicyId(int customerPolicyId)
        {
            try
            {
             
                return Ok(
                     _premiumTransactionService.FindByCustomerPolicyId(customerPolicyId)
                    );
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet("premium-transactions-by-customerid/{customerId}")]
        [Produces("application/json")]
        public IActionResult FindByCustomerId(int customerId)
        {
            try
            {
                return Ok(_premiumTransactionService.FindByCustomerId(customerId));
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
                    Result = _premiumTransactionService.Count()
                });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}