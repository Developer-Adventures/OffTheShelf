namespace DeveloperAdventures.OffTheSelf.Tests.Encryption
{
    using DeveloperAdventures.OffTheSelf.Encryption;

    using NUnit.Framework;

    [TestFixture]
    public class SHA512CryptoProviderTests
    {
        private SHA512CryptoProvider sut;

        [SetUp]
        public void SetUp()
        {
            sut = new SHA512CryptoProvider();
        }

        [TearDown]
        public void TearDown()
        {
            sut = null;
        }

        [Test]
        public void CanGetSalt()
        {
            // Arrange
            var size = 100;
            var size2 = 10;

            // Act
            var salt = sut.GetSalt(size);
            var salt2 = sut.GetSalt(size2);

            // Assert
            Assert.IsNotNullOrEmpty(salt);
            Assert.IsNotNullOrEmpty(salt2);
            Assert.AreNotEqual(salt, salt2);
        }

        [Test]
        public void CanEncrypt()
        {
            // Arrange
            var rawString = "StringToEncrypt";
            var salt = sut.GetSalt(100);
            var saltedString = salt + rawString;

            // Act
            var hashed = sut.Encrypt(saltedString);

            // Assert
            Assert.AreNotSame(saltedString, hashed);
            Assert.AreEqual(new SHA512CryptoProvider().Encrypt(saltedString), hashed);
        }
    }
}