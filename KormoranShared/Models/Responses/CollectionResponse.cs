using System.Collections.Generic;
using System.Text.Json;

namespace KormoranShared.Models.Responses
{
    public class CollectionResponse<T> : BasicResponse
    {
        public List<T> Collection { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(Collection) ?? string.Empty;
        }
    }
}