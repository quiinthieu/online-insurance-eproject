using Demo.Middlewares;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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


            services.AddControllers().AddNewtonsoftJson(options =>
             options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            // Add authentication service
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option => { option.Cookie.Name = "authentication"; });

            // Connect to DB
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"].ToString();
            services.AddDbContext<DatabaseContext>(option =>
                option.UseLazyLoadingProxies().UseSqlServer(connectionString));

            // Add services to communicate with DB
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<ICredentialService, CredentialService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerPolicyService, CustomerPolicyService>();
            services.AddScoped<IInsuranceTypeService, InsuranceTypeService>();
            services.AddScoped<IPolicyService, PolicyService>();
            services.AddScoped<IPremiumTransactionService, PremiumTransactionService>();
            services.AddScoped<IPremiumTypeService, PremiumTypeService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");
            app.UsePathBase("/api");
            app.UseCors(builder => builder
             .AllowAnyHeader()
             .AllowAnyMethod()
             .SetIsOriginAllowed((host) => true)
             .AllowCredentials()
         );
            // app.UseHttpsRedirection();

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