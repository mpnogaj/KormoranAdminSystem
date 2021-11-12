using Microsoft.AspNetCore.Mvc;

namespace KormoranAdminSystemRevamped.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PingController : ControllerBase
	{
		[HttpGet]
		public string Ping()
		{
			return "Pong";
		}
	}
}
