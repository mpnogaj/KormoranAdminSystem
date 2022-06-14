using KormoranWeb.Contexts;
using KormoranWeb.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace KormoranWeb
{
	public static class Program
	{
		private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllersWithViews();
			services.AddDbContext<KormoranContext>(options =>
			{
				options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
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
						ValidIssuer = configuration["JWT:Issuer"],
						ValidAudience = configuration["JWT:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
							configuration["JWT:Key"])
						)
					};

					options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							if (context.Request.Cookies.ContainsKey("Authorization"))
							{
								context.Token = context.Request.Cookies["Authorization"];
							}
							return Task.CompletedTask;
						}
					};
				});

			services.AddScoped<Services.ILogger, Logger>();
			services.AddControllersWithViews().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
			});
		}

		private static void ConfigureApp(WebApplication app, IHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			if (!app.Environment.IsDevelopment())
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			//app.UseHttpsRedirection();
			app.UseStaticFiles();
			

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action=Index}/{id?}");

			app.MapFallbackToFile("index.html");
		}

		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			ConfigureServices(builder.Services, builder.Configuration);
			var app = builder.Build();
			ConfigureApp(app, app.Environment);
			app.Run();
		}
	}
}