namespace KormoranShared.Models.Responses
{
    public class BasicResponse
    {
        public bool Error { get; set; }
        public string? Message { get; set; }

        public override string ToString()
        {
            return Message ?? string.Empty;
        }
    }
}
