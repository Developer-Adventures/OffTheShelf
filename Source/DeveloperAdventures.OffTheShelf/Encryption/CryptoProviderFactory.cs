namespace DeveloperAdventures.OffTheShelf.Encryption
{
    using DeveloperAdventures.OffTheSelf.Encryption;
    using DeveloperAdventures.OffTheShelf.Encryption.Interfaces;

    public class CryptoProviderFactory : ICryptoProviderFactory
    {
        #region Public Methods and Operators

        public ICryptoProvider GetProvider(SHACryptoStrength strength)
        {
            switch (strength)
            {
                case SHACryptoStrength.SHA256:
                {
                    return new SHA256CryptoProvider();
                }
                case SHACryptoStrength.SHA512:
                {
                    return new SHA512CryptoProvider();
                }
            }

            return new SHA512CryptoProvider();
        }

        public ICryptoProvider GetProvider(CryptoProviderType providerType)
        {
            switch (providerType)
            {
                case CryptoProviderType.AES:
                {
                    return new AESCryptoProvider(this.key, this.iv);
                }
                case CryptoProviderType.SHA:
                {
                    return new SHA256CryptoProvider();
                }
            }

            return new AESCryptoProvider(this.key, this.iv);
        }

        public ICryptoProvider GetProvider(CryptoProviderType providerType, SHACryptoStrength strength)
        {
            switch (providerType)
            {
                case CryptoProviderType.AES:
                {
                    return new AESCryptoProvider(this.key, this.iv);
                }
                case CryptoProviderType.SHA:
                {
                    if (strength == SHACryptoStrength.SHA256)
                    {
                        return new SHA256CryptoProvider();
                    }

                    return new SHA512CryptoProvider();
                }
            }

            return new AESCryptoProvider(this.key, this.iv);
        }

        public ICryptoProvider GetProvider(byte[] key, byte[] iv)
        {
            return new AESCryptoProvider(key, iv);
        }

        #endregion

        #region Fields

        private readonly byte[] iv = { 146, 64, 191, 111, 23, 3, 113, 119, 131, 221, 121, 212, 179, 32, 114, 255 };

        private readonly byte[] key = { 123, 150, 19, 11, 24, 26, 85, 45, 114, 184, 201, 162, 37, 212, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 118, 131, 236, 53, 109 };

        #endregion
    }
}