using System;
using System.Diagnostics;
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
        private IAgentService _agentService;
        private ICredentialService _credentialService;
        private IClaimService _claimService;


        public CustomerPolicyController(IClaimService claimService,ICredentialService credentialService,IAgentService agentService,ICustomerPolicyService customerPolicyService, IPolicyService policyService, ICustomerService customerService, IPremiumTransactionService premiumTransactService)
        {
            _customerPolicyService = customerPolicyService;
            _policyService = policyService;
            _customerService = customerService;
            _premiumTransactService = premiumTransactService;
            _agentService = agentService;
            _credentialService = credentialService;
            _claimService = claimService;
        }

        [HttpGet("all-customer-policies")]
        [Produces("application/json")]
        public IActionResult FindAll()
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
        public IActionResult FindByCustomerId(int customerId)
        {
            return Ok(_customerPolicyService.FindByCustomerId(customerId));

            /*		try
                    {
                        return Ok(_customerPolicyService.FindByCustomerId(customerId));
                    }
                    catch (Exception e)
                    {
                        return BadRequest();
                    }*/
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
        public IActionResult Create([FromBody] BuyPolicy customerPolicy)
        {
           /* try
            {*/
                var AGENT_ID = 1;

               _customerService.Update(customerPolicy.Id, new Customer
            {
                Name = customerPolicy.Name,
                Birthday = customerPolicy.Birthday,
                Gender = customerPolicy.Gender,
                Street = customerPolicy.Street,
                City = customerPolicy.City,
                State = customerPolicy.State,
                ZipCode = customerPolicy.ZipCode,
                Occupation = customerPolicy.Occupation,
                CredentialId = customerPolicy.CredentialId,
                CitizenId = customerPolicy.CitizenId,
            });


                customerPolicy.PolicyId.ForEach(policyId =>
                {
                // Lay ve term tu policy thong qua service vi doi tuong customerPolicy co Policy null
                int id = policyId;
                    dynamic policy = _policyService.FindById(id);
                    int term = policy.Term;
                    var cusPolicy = new CustomerPolicy();

                    cusPolicy.PolicyId = id;

                    cusPolicy.PremiumTypeId = customerPolicy.PremiumTypeId;

                // Tinh toan premium amount
                cusPolicy.PremiumAmount = policy.Term * policy.Amount;

                // Gan startdate
                cusPolicy.StartDate = DateTime.Now;

                // Gan enddate
                cusPolicy.EndDate = cusPolicy.StartDate.Value.AddYears((int)term);


                // Gan customer
                /*dynamic loginAccount = HttpContext.Items["credential"];*/

                    int credenId = customerPolicy.CredentialId;

                    dynamic customer = _customerService.FindByCredentialId(credenId);

                    int customerId = customer.Id;

                    cusPolicy.CustomerId = customerId;

                    dynamic cre = _credentialService.FindById(credenId);
                    cusPolicy.AgentId = _agentService.FindByBranchId(cre.BranchId).Id;

                // Them customerpolicy
                   var createdCusPolicy =   _customerPolicyService.Create(cusPolicy);

                    var pts = PremiumHelper.PremiumTransactionScheludler(term, cusPolicy);

                   var transaction =  _premiumTransactService.Create(pts);

                    var totalTrans = transaction.Sum(trans => trans.Amount);
                    var rateTrans = (decimal) ((policy.InterestRate/100)+1);
                    //create claim
                    var claim = new Claim();
                    claim.CustomerPolicyId = createdCusPolicy.Id;
                    claim.Amount = totalTrans * rateTrans;
                    _claimService.Create(claim);
                });


                return Ok(new
            {
                msg = "Successfully added"
            });
        /*    }
            catch (Exception ex)
            {
                return BadRequest();
            }*/
        }

        [HttpGet("count")]
        [Produces("application/json")]
        public IActionResult Count()
        {
            try
            {
                return Ok(new
                {
                    Result = _customerPolicyService.Count()
                });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}