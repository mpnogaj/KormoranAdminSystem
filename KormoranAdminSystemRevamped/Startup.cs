using System.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;
using KormoranAdminSystemRevamped.Data;
using Microsoft.EntityFrameworkCore;

namespace KormoranAdminSystemRevamped
{
    public class Startup
    {
        private const string PolicyName = "KormoranPolicy";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
	        Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddDbContext<MyDbContext>(options =>
	        {
		        options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
	        });
            services.AddControllersWithViews();
            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors(PolicyName);

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = Path.Join(env.ContentRootPath, "ClientApp");

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer("start");
                }
            });
        }
    }
}
