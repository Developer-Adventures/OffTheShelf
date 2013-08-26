namespace DeveloperAdventures.OffTheSelf.Tests.Encryption
{
    using DeveloperAdventures.OffTheSelf.Encryption;

    using NUnit.Framework;

    [TestFixture]
    public class SHACryptoProviderFactoryTests
    {
        private SHACryptoProviderFactory sut;

        [SetUp]
        public void SetUp()
        {
            this.sut = new SHACryptoProviderFactory();
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
            var provider = sut.GetProvider(strength);

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
            var provider = sut.GetProvider(strength);

            // Assert
            Assert.IsInstanceOf<SHA512CryptoProvider>(provider);
            Assert.IsNotInstanceOf<SHA256CryptoProvider>(provider);
        }
    }
}