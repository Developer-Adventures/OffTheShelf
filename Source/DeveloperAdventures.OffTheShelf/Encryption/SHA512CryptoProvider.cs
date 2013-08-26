namespace DeveloperAdventures.OffTheSelf.Encryption
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using DeveloperAdventures.OffTheSelf.Encryption.Interfaces;

    public class SHA512CryptoProvider : ISHACryptoProvider
    {
        private RNGCryptoServiceProvider rngProvider;
        private SHA512 sha512;
        private UTF8Encoding encoding;

        public SHA512CryptoProvider()
        {
            sha512 = SHA512.Create();
            encoding = new UTF8Encoding();
            rngProvider = new RNGCryptoServiceProvider();
        }

        public string Encrypt(string text)
        {
            byte[] result = sha512.ComputeHash(encoding.GetBytes(text));
            return Encoding.UTF8.GetString(result);
        }

        public string GetSalt(int size)
        {
            var buffer = new byte[size];
            rngProvider.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        public void Dispose()
        {
            if (sha512 != null)
            {
                sha512.Dispose();
            }

            if (this.rngProvider != null)
            {
                this.rngProvider.Dispose();
            }
        }
    }
}