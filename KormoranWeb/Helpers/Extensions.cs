using System.Security.Cryptography;
using System.Text;

namespace KormoranWeb.Helpers
{
    public static class Extensions
    {
        public static string Sha256(this string s)
        {
            StringBuilder sb = new();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(s));

                foreach (var b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}