namespace KormoranMobile.Maui.Helpers
{
	public static class TypeHelper
	{
		public static bool CheckType(object? obj, Type type, bool canBeNull = true)
		{
			if(obj == null)
			{
				return canBeNull;
			}
			return obj.GetType() == type;
		}
	}
}
