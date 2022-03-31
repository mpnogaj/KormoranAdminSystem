using KormoranAdminSystemRevamped.Contexts;
using KormoranAdminSystemRevamped.Models;
using KormoranAdminSystemRevamped.Models.Responses;
using KormoranAdminSystemRevamped.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KormoranAdminSystemRevamped.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class DisciplinesController : ControllerBase
	{
		private readonly KormoranContext _db;

		public DisciplinesController(KormoranContext db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<JsonResult> GetDisciplines()
		{
			try
			{
				var disciplines = await _db.Disciplines.ToListAsync();
				return new JsonResult(new CollectionResponse<Discipline>
				{
					Message = Resources.operationSuccessfull,
					Error = false,
					Collection = disciplines
				});
			}
			catch
			{
				return new JsonResult(new CollectionResponse<Discipline>
				{
					Message = Resources.serverError,
					Error = true,
					Collection = null
				});
			}
		}
	}
}
