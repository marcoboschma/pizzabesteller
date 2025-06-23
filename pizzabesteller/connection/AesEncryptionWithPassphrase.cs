using System.IO;
using System.Security.Cryptography;

namespace pizzabesteller.connection
{
    public class AesEncryptionWithPassphrase
    {
        private const int KeySize = 16; // AES-128 (16 bytes)
        private const int SaltSize = 16; // 16 bytes for the salt
        private const int Iterations = 100000; // Number of PBKDF2 iterations

        // Derive a key from the passphrase and salt
        private static byte[] DeriveKey(string passphrase, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(passphrase, salt, Iterations))
            {
                return pbkdf2.GetBytes(KeySize);
            }
        }

        // Encrypt a plaintext message with a passphrase
        public static byte[] Encrypt(string plaintext, string passphrase)
        {
            using (Aes aes = Aes.Create())
            {
                // Generate a random salt
                byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

                // Derive the encryption key
                byte[] key = DeriveKey(passphrase, salt);

                // Configure AES
                aes.Key = key;
                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (var memoryStream = new MemoryStream())
                {
                    // Write the salt and IV first (to be used for decryption)
                    memoryStream.Write(salt, 0, salt.Length);
                    memoryStream.Write(iv, 0, iv.Length);

                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoStream))
                    {
                        // Write the plaintext
                        writer.Write(plaintext);
                    }

                    // Return the complete encrypted message
                    return memoryStream.ToArray();
                }
            }
        }

        // Decrypt an encrypted message with a passphrase
        public static string Decrypt(byte[] cipherData, string passphrase)
        {
            using (Aes aes = Aes.Create())
            {
                using (var memoryStream = new MemoryStream(cipherData))
                {
                    // Read the salt (first 16 bytes)
                    byte[] salt = new byte[SaltSize];
                    memoryStream.Read(salt, 0, salt.Length);

                    // Derive the encryption key
                    byte[] key = DeriveKey(passphrase, salt);

                    // Read the IV (next 16 bytes)
                    byte[] iv = new byte[aes.BlockSize / 8];
                    memoryStream.Read(iv, 0, iv.Length);

                    aes.Key = key;
                    aes.IV = iv;

                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var reader = new StreamReader(cryptoStream))
                    {
                        // Read and return the decrypted plaintext
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}