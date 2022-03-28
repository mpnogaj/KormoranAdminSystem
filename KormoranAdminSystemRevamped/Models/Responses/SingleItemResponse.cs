namespace KormoranAdminSystemRevamped.Models.Responses
{
	public record SingleItemResponse<T> : BasicResponse
	{
		public T? Data { get; set; }
	}
}
