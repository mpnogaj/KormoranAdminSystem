using KormoranShared.Models.Responses;

namespace KormoranWeb.Models.Responses
{
	public class AuthenticateResponse : BasicResponse
    {
    	public string Token { get; set; }
    }
}

