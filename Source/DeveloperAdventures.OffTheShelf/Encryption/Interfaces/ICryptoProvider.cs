namespace DeveloperAdventures.OffTheShelf.Encryption.Interfaces
{
    using System;

    public interface ICryptoProvider : IDisposable
    {
        string Decrypt(string text);

        string Encrypt(string text);

        string GetSalt(int size);
    }
}