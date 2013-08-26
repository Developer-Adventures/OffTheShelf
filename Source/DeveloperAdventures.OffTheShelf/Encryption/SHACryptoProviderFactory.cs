namespace DeveloperAdventures.OffTheSelf.Encryption
{
    using DeveloperAdventures.OffTheSelf.Encryption.Interfaces;

    public class SHACryptoProviderFactory : ISHACryptoProviderFactory
    {
        public ISHACryptoProvider GetProvider(SHACryptoStrength strength)
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

            return new SHA256CryptoProvider();
        }
    }
}