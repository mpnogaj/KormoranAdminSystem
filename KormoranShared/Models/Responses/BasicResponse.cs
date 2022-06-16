using System.Diagnostics.CodeAnalysis;

namespace KormoranShared.Models.Responses
{
	public class BasicResponse
	{
		public bool Error { get; set; }

		[NotNull]
		public string Message { get; set; } = string.Empty;

		public override string ToString()
		{
			return Message;
		}
	}
}