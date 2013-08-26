/*
 * Code based off of answer at Stack Overflow:
 * http://stackoverflow.com/a/7314406/70608
 * 
 */


using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DeveloperAdventures.OffTheSelf.Encryption
{
    public class AESCryptoProvider : IDisposable
    {
        Rijndael rijndael;
        UTF8Encoding encoding;

        public AESCryptoProvider(byte[] key, byte[] vector)
        {
            encoding = new UTF8Encoding();
            rijndael = Rijndael.Create();
            rijndael.Key = key;
            rijndael.IV = vector;
        }

        public byte[] Encrypt(string valueToEncrypt)
        {
            var bytes = encoding.GetBytes(valueToEncrypt);
            using (var encryptor = rijndael.CreateEncryptor())
            using (var stream = new MemoryStream())
            using (var crypto = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
            {
                crypto.Write(bytes, 0, bytes.Length);
                crypto.FlushFinalBlock();
                stream.Position = 0;
                var encrypted = new byte[stream.Length];
                stream.Read(encrypted, 0, encrypted.Length);
                return encrypted;
            }
        }

        public string Decrypt(byte[] encryptedValue)
        {
            using (var decryptor = rijndael.CreateDecryptor())
            using (var stream = new MemoryStream())
            using (var crypto = new CryptoStream(stream, decryptor, CryptoStreamMode.Write))
            {
                crypto.Write(encryptedValue, 0, encryptedValue.Length);
                crypto.FlushFinalBlock();
                stream.Position = 0;
                var decryptedBytes = new Byte[stream.Length];
                stream.Read(decryptedBytes, 0, decryptedBytes.Length);
                return encoding.GetString(decryptedBytes);
            }
        }

        public void Dispose()
        {
            if (rijndael != null)
            {
                rijndael.Dispose();
            }
        }
    }
}