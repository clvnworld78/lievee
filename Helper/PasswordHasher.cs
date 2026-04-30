using lievee.Models;
using System.Security.Cryptography;
using System.Text;

namespace lievee.Helper
{
    public static class PasswordHasher
    {
        public static byte[] HashPassword(string password)
        {
            var inputByte = Encoding.UTF8.GetBytes(password);
            return SHA256.HashData(inputByte);
        }

        public static bool VerifyPassword(string password, byte[] hashedPassword)
        {
            return hashedPassword.SequenceEqual(PasswordHasher.HashPassword(password));
        }
    }
}
