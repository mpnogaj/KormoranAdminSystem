using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
