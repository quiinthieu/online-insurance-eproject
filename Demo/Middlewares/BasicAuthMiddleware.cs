using Demo.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, ICredentialService credentialService)
        {

            if (httpContext.Request.Headers["Authorization"].Count > 0 && httpContext.Items["credential"] == null)
            {
                Debug.WriteLine(httpContext.Request.Headers["Authorization"]
                    .ToString());

                var basicAuthHeader = httpContext.Request.Headers["Authorization"]
                    .ToString().Replace("Basic ", "");
                var credential = Encoding.GetEncoding("UTF-8").GetString(
                    Convert.FromBase64String(basicAuthHeader));

                var credentialInfo = credential.Split(new char[] { ':' });
                var email = credentialInfo[0];
                var password = credentialInfo[1];
                dynamic loginAccount = credentialService.FindByEmailAndPassword(email, password);

                if (loginAccount == null || !loginAccount.Status)
                {
                    throw new UnauthorizedAccessException();
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity
               (credentialService.GetUserClaims(loginAccount),
              CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                httpContext.Items["credential"] = loginAccount;
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BasicAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthMiddleware>();
        }
    }
}
