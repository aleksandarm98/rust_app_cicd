using System;
using System.Security.Cryptography;
using System.Text;

namespace PetMeetApp.Common.Extensions
{
    public static class EncodingExtension
    {
        public static string HashToSHA256(string value)
        {
            StringBuilder Sb = new StringBuilder();
            string salt = "p10m*/t";
            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;

                var saltBytes = enc.GetBytes(salt);
                var valueBytes = enc.GetBytes(value);

                byte[] saltedBytes = new byte[saltBytes.Length + valueBytes.Length];

                for (int i = 0; i < saltBytes.Length; i++)
                {
                    saltedBytes[i] = saltBytes[i];
                }
                for (int i = 0; i < valueBytes.Length; i++)
                {
                    saltedBytes[i + saltBytes.Length] = valueBytes[i];
                }

                byte[] result = hash.ComputeHash(saltedBytes);

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public static string GenerateRandomCode()
        {
            string code = "";
            Random rnd = new();

            for (int i = 0; i < 6; i++)
            {
                code += rnd.Next(1, 10);
            }

            return code;
        }
    }
}
