namespace DeveloperAdventures.OffTheShelf.Encryption
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using DeveloperAdventures.OffTheShelf.Encryption.Interfaces;

    public class SHA512CryptoProvider : ICryptoProvider
    {
        #region Public Methods and Operators

        public string Decrypt(string text)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (this.sha512 != null)
            {
                this.sha512.Dispose();
            }

            if (this.rngProvider != null)
            {
                this.rngProvider.Dispose();
            }
        }

        public string Encrypt(string text)
        {
            byte[] result = this.sha512.ComputeHash(this.encoding.GetBytes(text));
            return Encoding.UTF8.GetString(result);
        }

        public string GetSalt(int size)
        {
            var buffer = new byte[size];
            this.rngProvider.GetBytes(buffer);

            return Convert.ToBase64String(buffer);
        }

        #endregion

        #region Fields

        private readonly UTF8Encoding encoding;

        private readonly RNGCryptoServiceProvider rngProvider;

        private readonly SHA512 sha512;

        #endregion

        #region Constructors and Destructors

        public SHA512CryptoProvider()
        {
            this.sha512 = SHA512.Create();
            this.encoding = new UTF8Encoding();
            this.rngProvider = new RNGCryptoServiceProvider();
        }

        #endregion
    }
}