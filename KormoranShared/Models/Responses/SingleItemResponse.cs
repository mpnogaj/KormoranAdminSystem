using System.Text.Json;

namespace KormoranShared.Models.Responses
{
	public class SingleItemResponse<T> : BasicResponse
	{
		public T Data { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize(Data) ?? string.Empty;
		}
	}
}