namespace DeveloperAdventures.OffTheSelf.Encryption
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using DeveloperAdventures.OffTheSelf.Encryption.Interfaces;

    public class SHA256CryptoProvider : ISHACryptoProvider
    {
        private RNGCryptoServiceProvider rngProvider;
        private SHA256 sha256;
        private UTF8Encoding encoding;

        public SHA256CryptoProvider()
        {
            this.sha256 = SHA256.Create();
            encoding = new UTF8Encoding();
            rngProvider = new RNGCryptoServiceProvider();
        }

        public string Encrypt(string text)
        {
            byte[] result = sha256.ComputeHash(encoding.GetBytes(text));
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
            if (this.sha256 != null)
            {
                this.sha256.Dispose();
            }

            if (this.rngProvider != null)
            {
                this.rngProvider.Dispose();
            }
        }
    }
}