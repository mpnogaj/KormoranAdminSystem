using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KormoranMobile.Maui.Helpers
{
	public static class AuthHelper
	{
		public static string? Token { get; set; } = null;
		public static bool IsLoggedIn =>
			Token != null;
	}
}
