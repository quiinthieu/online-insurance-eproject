using System;
using Demo.Models;
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
        public IActionResult FindAll()
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

        [HttpPost("create")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult Create([FromBody] InsuranceType insuranceType)
        {
            try
            {
                return Ok(_insuranceTypeService.Create(insuranceType));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        //update
        [HttpPut("update/{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Update([FromBody] InsuranceType insuranceType, int id)
        {
            /*	try
                {*/
            var insType = _insuranceTypeService.FindById(id);

            var updatedInsType = _insuranceTypeService.Update(new InsuranceType
            {
                Id = insuranceType.Id,
                Name = insuranceType.Name,
                Description = insuranceType.Description,
            });
            return Ok(updatedInsType);

            /*	}
                catch (Exception e)
                {
                    return BadRequest(e);
                }*/
        }

    }
}