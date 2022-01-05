namespace KormoranAdminSystemRevamped.Models.Responses
{
	public record BasicResponse
	{
		public bool Error { get; set;}
		public string Message { get; set;} = string.Empty;
	}
}
