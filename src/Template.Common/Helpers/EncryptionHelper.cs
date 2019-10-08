using System.Security.Cryptography;
using System.Text;

namespace Template.Common.Helpers
{
    public static class EncryptionHelper
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("x2"));

                return sb.ToString();
            }
        }

        public static string ComputeHmacSha256Hash(this string valueToHash, string key)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] valueBytes = Encoding.ASCII.GetBytes(valueToHash);
            byte[] tokenBytes = new HMACSHA256(keyBytes).ComputeHash(valueBytes);

            StringBuilder token = new StringBuilder();
            foreach (byte b in tokenBytes)
            {
                token.AppendFormat("{0:x2}", b);
            }

            return token.ToString();
        }
    }
}
