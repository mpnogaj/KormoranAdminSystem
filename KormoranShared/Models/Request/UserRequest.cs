using System.ComponentModel.DataAnnotations;

namespace KormoranWeb.Models.Request
{
    public class UserRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
