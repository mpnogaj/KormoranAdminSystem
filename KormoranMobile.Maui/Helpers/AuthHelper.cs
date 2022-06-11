namespace KormoranMobile.Maui.Helpers
{
	public static class AuthHelper
	{
		public static string? Token { get; set; }
		public static bool IsLoggedIn =>
			Token != null;
	}
}
