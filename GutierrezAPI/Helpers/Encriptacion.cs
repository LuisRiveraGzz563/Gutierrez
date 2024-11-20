using System.Security.Cryptography;
using System.Text;

namespace GutierrezAPI.Helpers
{
    public class Encriptacion
    {
        public static string EncriptarSha512(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var result = SHA512.HashData(bytes);
            return Convert.ToHexString(result).ToLower();
        }

        //reachndale
    }
}
