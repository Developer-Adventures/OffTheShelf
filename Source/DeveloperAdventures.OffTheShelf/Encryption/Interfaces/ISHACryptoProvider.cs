namespace DeveloperAdventures.OffTheSelf.Encryption.Interfaces
{
    using System;

    public interface ISHACryptoProvider : IDisposable
    {
        string Encrypt(string text);

        string GetSalt(int size);
    }
}