using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BusinessLogic
{
    public class Security
    {
        public static string HashingSHA256(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;

            using (var sha256 = SHA256.Create())
            {
                var hashedInputBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                var hashedInputStringBuilder = new StringBuilder(hashedInputBytes.Length * 2);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }

        public static string GeneratePassword(string password, string salt)
        {
            password += salt;
            return HashingSHA256(password);
        }

        public static bool VerifyPassword(string password, string dbPassword, string dbSalt)
        {
            password += dbSalt;
            string newPassword = HashingSHA256(password);
            bool result = dbPassword == newPassword;
            return result;
        }
    }
}
