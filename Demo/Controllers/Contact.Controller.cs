using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Demo.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private IConfiguration configuration;

        public ContactController(IConfiguration _configuration)
        {
            configuration = _configuration;
        } 

        [Produces("application/json")]
        [HttpPost("send")]
        public IActionResult Send()
        {
            //var s = "Subject" + subject + "<br>" + "Content" + content;
            
            var mailHelper = new MailHelper(configuration);
            try
            {
                var result = "";
                if (mailHelper.Send("testme2404@gmail.com", configuration["Gmail:Username"],"Test Mail", "Hello... Thanks"))
                {
                    result = "Success";

                }
                else
                {
                    result = "Failed";
                }
                    
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
