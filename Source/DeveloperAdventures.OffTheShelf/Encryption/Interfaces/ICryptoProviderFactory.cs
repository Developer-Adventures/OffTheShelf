﻿namespace DeveloperAdventures.OffTheShelf.Encryption.Interfaces
{
    using DeveloperAdventures.OffTheSelf.Encryption;

    public interface ICryptoProviderFactory
    {
        ICryptoProvider GetProvider(SHACryptoStrength strength);

        ICryptoProvider GetProvider(CryptoProviderType providerType);

        ICryptoProvider GetProvider(CryptoProviderType providerType, SHACryptoStrength strength);
    }
}