using KormoranAdminSystemRevamped.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KormoranAdminSystemRevamped.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TournamentsController : ControllerBase
	{
		private readonly KormoranContext _db;
		public TournamentsController(KormoranContext dp)
		{
			_db = dp;
		}

		[HttpGet]
		public async Task<IActionResult> Tournaments()
		{
			var tournamentList = await _db.Tournaments.ToListAsync();
			return Ok(JsonConvert.SerializeObject(tournamentList));
		}
	}
}
