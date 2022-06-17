namespace KormoranShared.Helpers
{
	public static class Extensions
	{
		public static string SerializeDate(this DateTime d)
		{
			//15.06.2022 21:08:39
			return $"{d:dd.MM.yyyy} {d:HH:mm:ss}";
		}
	}
}
