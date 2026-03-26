namespace Equant.SAV2000.ComponentLibrary.Common.Helper
{
    /// <summary>
    /// Provides encryption and decryption utilities.
    /// </summary>
    public static class Crypt
    {
        /// <summary>
        /// Encrypts the specified plain text value.
        /// </summary>
        /// <param name="value">The plain text value to encrypt.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            // Stub implementation: returns a Base64-encoded version as placeholder
            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Decrypts the specified encrypted value.
        /// </summary>
        /// <param name="value">The encrypted value to decrypt.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            // Stub implementation: returns a Base64-decoded version as placeholder
            var bytes = System.Convert.FromBase64String(value);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}
