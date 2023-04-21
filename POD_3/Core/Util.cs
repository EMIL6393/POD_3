using System.Security.Cryptography;
using System.Text;

namespace POD_3.Core
{
    public class Util
    {
        public static string PasswordHashing(string password)
        {
            var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedByte = sha256.ComputeHash(passwordBytes);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < hashedByte.Length; i++)
                stringBuilder.Append(hashedByte[i].ToString("X2"));

            return stringBuilder.ToString();
        }
    }
}
