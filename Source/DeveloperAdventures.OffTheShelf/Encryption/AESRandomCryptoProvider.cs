using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DeveloperAdventures.OffTheShelf.Encryption.Interfaces;

namespace DeveloperAdventures.OffTheShelf.Encryption
{
    public class AESRandomCryptoProvider : ICryptoProvider
    {
        Random random;
        Rijndael rijndael;
        UTF8Encoding encoding;
        byte[] _key;

        public AESRandomCryptoProvider(string key)
        {
            this.random = new Random();
            this.encoding = new UTF8Encoding();
            this.rijndael = Rijndael.Create();
            _key = Convert.FromBase64String(key);
        }

        public void Dispose()
        {
            if (this.rijndael != null)
            {
                this.rijndael.Dispose();
            }
        }

        public string GetSalt(int size)
        {
            throw new System.NotImplementedException();
        }

        public string Encrypt(string unencrypted)
        {
            var vector = new byte[16];
            this.random.NextBytes(vector);
            var cryptogram = vector.Concat(this.Encrypt(this.encoding.GetBytes(unencrypted), vector));
            return Convert.ToBase64String(cryptogram.ToArray());
        }

        public string Decrypt(string encrypted)
        {
            var cryptogram = Convert.FromBase64String(encrypted);
            if (cryptogram.Length < 17)
            {
                throw new ArgumentException("Not a valid encrypted string", "encrypted");
            }

            var vector = cryptogram.Take(16).ToArray();
            var buffer = cryptogram.Skip(16).ToArray();
            return this.encoding.GetString(this.Decrypt(buffer, vector));
        }

        private byte[] Encrypt(byte[] buffer, byte[] vector)
        {
            var encryptor = this.rijndael.CreateEncryptor(this._key, vector);
            return this.Transform(buffer, encryptor);
        }

        private byte[] Decrypt(byte[] buffer, byte[] vector)
        {
            var decryptor = this.rijndael.CreateDecryptor(this._key, vector);
            return this.Transform(buffer, decryptor);
        }

        private byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }

            return stream.ToArray();
        }
    }
}