using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using KormoranWeb.Contexts;
using KormoranWeb.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace KormoranWeb
{
	public class Startup
	{
		private const string PolicyName = "KormoranPolicy";
		private IConfiguration Configuration { get; }

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

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = Configuration["JWT:Issuer"],
						ValidAudience = Configuration["JWT:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
							Configuration["JWT:Key"])
						)
					};
				});
			
			
			services.AddSingleton<ISessionManager, SessionManager>();
			services.AddScoped<ILogger, Logger>();
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
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseStaticFiles();
			app.UseSpaStaticFiles();
			app.UseCors(PolicyName);

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					"default",
					"{controller}/{action=Index}");
			});

			Console.WriteLine(Path.Join(env.ContentRootPath, "ClientApp"));

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = Path.Join(env.ContentRootPath, "ClientApp");
				spa.Options.StartupTimeout = TimeSpan.FromSeconds(50);
				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer("start");
				}
			});
		}
	}
}
