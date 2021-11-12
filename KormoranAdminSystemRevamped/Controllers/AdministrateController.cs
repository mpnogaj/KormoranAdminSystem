using KormoranAdminSystemRevamped.Contexts;
using KormoranAdminSystemRevamped.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KormoranAdminSystemRevamped.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdministrateController : ControllerBase
	{
		private readonly KormoranContext _db;

		public AdministrateController(KormoranContext db)
		{
			_db = db;
		}

		[HttpPost]
		public async Task<IActionResult> Administrate(AdministrateBodyModel model)
		{
			var u = await _db.Users.FirstOrDefaultAsync(x => x.Login == model.username);
			return u == null ?
				StatusCode(403) :
				StatusCode(u.PasswordHash.ToLower() == model.password.Sha256() ? 200 : 403);
		}
	}

	public class AdministrateBodyModel
	{
		public string username { get; set; } = "";
		public string password { get; set; } = "";
	}
}
