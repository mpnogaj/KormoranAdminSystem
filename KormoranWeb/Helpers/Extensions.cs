using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KormoranWeb.Helpers
{
	public static class Extensions
	{
		public static string Sha256(this string s)
		{
			StringBuilder sb = new();

			using (var hash = SHA256.Create())
			{
				byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(s));

				foreach (var b in result)
					sb.Append(b.ToString("x2"));
			}

			return sb.ToString();
		}

		public static string Serialize(this DateTime d)
		{
			//15.06.2022 21:08:39
			return $"{d:dd.MM.yyyy} {d:HH:mm:ss}";
		}

		public static string GetFullName(this ClaimsPrincipal user)
		{
			return (user.Identity == null || user.Identity.Name == null) 
				? "Anonymous" 
				: user.Identity.Name;
		}

		public static bool IsLoggedIn(this ClaimsPrincipal user)
		{
			return !(user.Identity == null || user.Identity.Name == null);
		}

	}
}