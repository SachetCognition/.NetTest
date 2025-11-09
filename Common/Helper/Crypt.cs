namespace Equant.SAV2000.ComponentLibrary.Common.Helper
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class Crypt
    {
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            var base64EncodedBytes = Convert.FromBase64String(cipherText);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
