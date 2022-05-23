using System;
using System.Collections.Generic;
using System.Text;

namespace KormoranShared.Models.Requests
{
    public class AddUserRequestModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
