namespace DeveloperAdventures.OffTheShelf.Tests.Encryption
{
    using DeveloperAdventures.OffTheSelf.Encryption;
    using DeveloperAdventures.OffTheShelf.Encryption;
    using DeveloperAdventures.OffTheShelf.Encryption.Interfaces;

    using NUnit.Framework;

    [TestFixture]
    public class CryptoProviderFactoryTests
    {
        private CryptoProviderFactory sut;

        [SetUp]
        public void SetUp()
        {
            this.sut = new CryptoProviderFactory();
        }

        [TearDown]
        public void TearDown()
        {
            this.sut = null;
        }

        [Test]
        public void CanGet256Provider()
        {
            // Arrange
            var strength = SHACryptoStrength.SHA256;

            // Act
            var provider = this.sut.GetProvider(strength);

            // Assert
            Assert.IsInstanceOf<SHA256CryptoProvider>(provider);
            Assert.IsNotInstanceOf<SHA512CryptoProvider>(provider);
        }

        [Test]
        public void CanGet512Provider()
        {
            // Arrange
            var strength = SHACryptoStrength.SHA512;

            // Act
            var provider = this.sut.GetProvider(strength);

            // Assert
            Assert.IsInstanceOf<SHA512CryptoProvider>(provider);
            Assert.IsNotInstanceOf<SHA256CryptoProvider>(provider);
        }

        [Test]
        public void CanGetAESProvider()
        {
            // Act
            var provider = this.sut.GetProvider(CryptoProviderType.AES);

            // Assert
            Assert.IsInstanceOf<AESCryptoProvider>(provider);
        }

        [Test]
        public void CanGetAESWhilePassingStrength()
        {
            // Act
            var provider = this.sut.GetProvider(CryptoProviderType.AES, SHACryptoStrength.SHA512);

            // Assert
            Assert.IsInstanceOf<AESCryptoProvider>(provider);
        }

        [Test]
        public void CanGetSHAPassingInStrength()
        {
            // Act
            var provider = this.sut.GetProvider(CryptoProviderType.SHA, SHACryptoStrength.SHA512);

            // Assert
            Assert.IsInstanceOf<SHA512CryptoProvider>(provider);
        }
    }
}