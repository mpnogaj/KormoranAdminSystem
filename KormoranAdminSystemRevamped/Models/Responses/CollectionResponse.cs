using System.Collections.Generic;

namespace KormoranAdminSystemRevamped.Models.Responses
{
	public record CollectionResponse<T> : BasicResponse
	{
		public List<T>? Collection { get; set; }
	}
}
