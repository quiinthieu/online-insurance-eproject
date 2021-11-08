using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Demo.Helpers
{
    public class MailHelper
    {
        private  IConfiguration _configuration;
        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public  bool Send(string from, string to, string subject, string body)
        {
            try
            { 
               
                var host = _configuration["Gmail:Host"];
                var port = int.Parse(_configuration["Gmail:Port"]);
                var username = _configuration["Gmail:Username"];
                var password = _configuration["Gmail:Password"];
                var enable = bool.Parse(_configuration["Gmail:SMTP:starttls:enable"]);
                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enable,
                    Credentials = new NetworkCredential(username, password)
                };
                var mailMessage = new MailMessage(from, to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch
            {
                throw new Exception("Sent email failed");
            }
        }
        public  bool Send(string from, string to, string subject, string body, string htmlAttachment)
        {
            try
            {
                var host = _configuration["Gmail:Host"];
                var port = int.Parse(_configuration["Gmail:Port"]);
                var username = _configuration["Gmail:Username"];
                var password = _configuration["Gmail:Password"];
                var enable = bool.Parse(_configuration["Gmail:SMTP:starttls:enable"]);
                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enable,
                    Credentials = new NetworkCredential(username, password)
                };
                var mailMessage = new MailMessage(from, to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                if (!string.IsNullOrEmpty(htmlAttachment))
                {
                    using (var stream = new MemoryStream())
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.Write(htmlAttachment);
                            writer.Flush();
                            stream.Position = 0;
                            mailMessage.Attachments.Add(new Attachment(stream, "orderInfo.html", "text/html"));
                            smtpClient.Send(mailMessage);
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
