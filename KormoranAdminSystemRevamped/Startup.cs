using System;
using KormoranAdminSystemRevamped.Contexts;
using KormoranAdminSystemRevamped.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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
			services.AddDbContext<KormoranContext>(options =>
			{
				options.UseMySql(
					Configuration.GetConnectionString("DefaultConnection"), 
					new MySqlServerVersion(new Version(8, 0, 27)));
			});
			services.Add(new ServiceDescriptor(typeof(ISessionManager), new SessionManager()));
			services.AddControllersWithViews().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
			});
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
