/*
 * Code from answer at Stack Overflow:
 * http://stackoverflow.com/a/7314406/70608
 * 
 */

namespace DeveloperAdventures.OffTheSelf.Tests.Encryption
{
    using DeveloperAdventures.OffTheSelf.Encryption;

    using NUnit.Framework;

    [TestFixture]
    public class AESCryptoProviderTests
    {
        [Test]
        public void CanDecrypt()
        {
            //These two values should not be hard coded in your code.
            byte[] key = { 251, 9, 67, 117, 237, 158, 138, 150, 255, 97, 103, 128, 183, 65, 76, 161, 7, 79, 244, 225, 146, 180, 51, 123, 118, 167, 45, 10, 184, 181, 202, 190 };
            byte[] vector = { 214, 11, 221, 108, 210, 71, 14, 15, 151, 57, 241, 174, 177, 142, 115, 137 };

            using (var rijndaelHelper = new AESCryptoProvider(key, vector))
            {
                var encrypt = rijndaelHelper.Encrypt("StringToEncrypt");
                var decrypt = rijndaelHelper.Decrypt(encrypt);
                Assert.AreEqual("StringToEncrypt", decrypt);
            }
        }

        [Test]
        public void CanEncrypt()
        {
            //These two values should not be hard coded in your code.
            byte[] key = { 251, 9, 67, 117, 237, 158, 138, 150, 255, 97, 103, 128, 183, 65, 76, 161, 7, 79, 244, 225, 146, 180, 51, 123, 118, 167, 45, 10, 184, 181, 202, 190 };
            byte[] vector = { 214, 11, 221, 108, 210, 71, 14, 15, 151, 57, 241, 174, 177, 142, 115, 137 };

            using (var rijndaelHelper = new AESCryptoProvider(key, vector))
            {
                var encrypt = rijndaelHelper.Encrypt("StringToEncrypt");
                Assert.AreNotEqual("StringToEncrypt", encrypt);
            }
        }
    }
}