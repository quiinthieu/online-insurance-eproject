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
        private IConfiguration _configuration;

        public ContactController(IConfiguration configuration)
        {
            _configuration = configuration;
        } 

        [Produces("application/json")]
        [HttpPost("send/{subject}/{content}")]
        public IActionResult Send(string subject, string content)
        {
            //var s = "Subject" + subject + "<br>" + "Content" + content;
  
         var mailHelper = new MailHelper(_configuration);
            try
            {
                var result = "";
                if (mailHelper.Send("testme2404@gmail.com", _configuration["Gmail:Username"],subject, content))
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
