using System.IO;
using Demo.Middlewares;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Demo
{
	public class Startup
	{
		private IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddControllers();

			// Add authentication service
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(option => { option.Cookie.Name = "authentication"; });

			// Connect to DB
			var connectionString = Configuration["ConnectionStrings:DefaultConnection"].ToString();
			services.AddDbContext<DatabaseContext>(option =>
				option.UseLazyLoadingProxies().UseSqlServer(connectionString));

			// Add services to communicate with DB
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<ICredentialService, CredentialService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseExceptionHandler("/error");

			app.UseHttpsRedirection();
			
			app.UseStaticFiles();

			app.UseMiddleware<CorsMiddleware>();

			app.UseMiddleware<BasicAuthMiddleware>();
			
			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

		}
	}
}