namespace DeveloperAdventures.OffTheSelf.Encryption.Interfaces
{
    public interface ISHACryptoProviderFactory
    {
        ISHACryptoProvider GetProvider(SHACryptoStrength strength);
    }
}