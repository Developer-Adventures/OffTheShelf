/*
 * Code based off of answer at Stack Overflow:
 * http://stackoverflow.com/a/7314406/70608
 * 
 */

namespace DeveloperAdventures.OffTheShelf.Encryption
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    using DeveloperAdventures.OffTheShelf.Encryption.Interfaces;

    public class AESCryptoProvider : ICryptoProvider
    {
        Rijndael rijndael;
        UTF8Encoding encoding;

        public AESCryptoProvider(byte[] key, byte[] vector)
        {
            this.encoding = new UTF8Encoding();
            this.rijndael = Rijndael.Create();
            this.rijndael.Key = key;
            this.rijndael.IV = vector;
        }

        public string Decrypt(string text)
        {
            using (var decryptor = this.rijndael.CreateDecryptor())
            using (var stream = new MemoryStream())
            using (var crypto = new CryptoStream(stream, decryptor, CryptoStreamMode.Write))
            {
                var encryptedValue = Convert.FromBase64String(text);
                crypto.Write(encryptedValue, 0, encryptedValue.Length);
                crypto.FlushFinalBlock();
                stream.Position = 0;
                var decryptedBytes = new Byte[stream.Length];
                stream.Read(decryptedBytes, 0, decryptedBytes.Length);
                return this.encoding.GetString(decryptedBytes);
            }
        }

        public string Encrypt(string text)
        {
            var bytes = this.encoding.GetBytes(text);
            using (var encryptor = this.rijndael.CreateEncryptor())
            using (var stream = new MemoryStream())
            using (var crypto = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
            {
                crypto.Write(bytes, 0, bytes.Length);
                crypto.FlushFinalBlock();
                stream.Position = 0;
                var encrypted = new byte[stream.Length];
                stream.Read(encrypted, 0, encrypted.Length);
                return Convert.ToBase64String(encrypted);
            }
        }

        public string GetSalt(int size)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (this.rijndael != null)
            {
                this.rijndael.Dispose();
            }
        }
    }
}