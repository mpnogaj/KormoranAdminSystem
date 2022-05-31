using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace KormoranShared.Helpers
{
	public static class JsonHelper
	{
		private readonly static JsonSerializerOptions options = new()
		{
			Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
		};

		public static string Serialize(this object obj)
		{
			return JsonSerializer.Serialize(obj, options: options);
		}

		public static T? Deserialize<T>(this string json)
		{
			return JsonSerializer.Deserialize<T>(json, options: options);
		}
	}
}