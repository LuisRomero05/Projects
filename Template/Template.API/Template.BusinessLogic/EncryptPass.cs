using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Template.BusinessLogic
{
    public class EncryptPass
    {
        public static string CreateRandomWordNumberCombination()
        {
            Random rnd = new Random();
            //Dictionary of strings
            string[] words = { "Bold", "Think", "Friend", "Pony", "Fall", "Easy" };
            //Random number from - to
            int randomNumber = rnd.Next(2000, 3000);
            //Create combination of word + number
            string randomString = $"{words[rnd.Next(0, words.Length)]}{randomNumber}";
            return randomString;

        }
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
            return dbPassword == HashingSHA256(password);
        }
    }
}
