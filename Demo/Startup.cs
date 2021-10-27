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
using Microsoft.Extensions.Hosting;

namespace Demo
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			// In production, the Angular files will be served from this directory
			/*services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });*/

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			   .AddCookie(option => {
				   option.Cookie.Name = "authentication";
			   });

			var connectionString = Configuration["ConnectionStrings:DefaultConnection"].ToString();

			services.AddDbContext<DatabaseContext>(option =>
				option.UseLazyLoadingProxies().UseSqlServer(connectionString));

			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<ICredentialService, CredentialService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{


			app.UseExceptionHandler("/error");

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			
			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			app.UseRouting();

			app.UseMiddleware<BasicAuthMiddleware>();

		

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

/*			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});*/
		}
	}
}