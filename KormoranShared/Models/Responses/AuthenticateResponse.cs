namespace KormoranShared.Models.Responses
{
	public class AuthenticateResponse : BasicResponse
	{
		public string Token { get; set; } = string.Empty;
	}
}