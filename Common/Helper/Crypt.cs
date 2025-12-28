using System.Security.Cryptography;
using System.Text;

namespace Equant.SAV2000.ComponentLibrary.Common.Helper;

/// <summary>
/// Provides encryption and decryption utilities.
/// Keys should be configured via environment variables or application configuration.
/// </summary>
public static class Crypt
{
    private static byte[]? _key;
    private static byte[]? _iv;

    private static byte[] Key => _key ??= GetKeyFromEnvironment("SAV2000_ENCRYPTION_KEY", 16);
    private static byte[] IV => _iv ??= GetKeyFromEnvironment("SAV2000_ENCRYPTION_IV", 16);

    private static byte[] GetKeyFromEnvironment(string envVarName, int requiredLength)
    {
        var value = Environment.GetEnvironmentVariable(envVarName);
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidOperationException($"Environment variable '{envVarName}' is not set. Please configure encryption keys via environment variables.");
        }
        var bytes = Encoding.UTF8.GetBytes(value);
        if (bytes.Length < requiredLength)
        {
            throw new InvalidOperationException($"Environment variable '{envVarName}' must be at least {requiredLength} characters.");
        }
        return bytes.Take(requiredLength).ToArray();
    }

    /// <summary>
    /// Encrypts the specified plain text.
    /// </summary>
    /// <param name="plainText">The plain text to encrypt.</param>
    /// <returns>The encrypted text as a Base64 string.</returns>
    public static string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
        {
            return plainText;
        }

        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var msEncrypt = new MemoryStream();
        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    /// <summary>
    /// Decrypts the specified cipher text.
    /// </summary>
    /// <param name="cipherText">The cipher text to decrypt (Base64 encoded).</param>
    /// <returns>The decrypted plain text.</returns>
    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
        {
            return cipherText;
        }

        var cipherBytes = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = IV;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var msDecrypt = new MemoryStream(cipherBytes);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }
}
