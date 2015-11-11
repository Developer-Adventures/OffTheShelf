/*
 * Code from answer at Stack Overflow:
 * http://stackoverflow.com/a/7314406/70608
 * 
 */

using System;

namespace DeveloperAdventures.OffTheShelf.Tests.Encryption
{
    using DeveloperAdventures.OffTheShelf.Encryption;

    using NUnit.Framework;

    [TestFixture]
    public class AESRandomCryptoProviderTests
    {
        const string RawValue = "StringToEncrypt";

        [Test]
        public void CanDecrypt()
        {
            //These two values should not be hard coded in your code.
            byte[] key = { 251, 9, 67, 117, 237, 158, 138, 150, 255, 97, 103, 128, 183, 65, 76, 161, 7, 79, 244, 225, 146, 180, 51, 123, 118, 167, 45, 10, 184, 181, 202, 190 };

            using (var rijndaelHelper = new AESRandomCryptoProvider(Convert.ToBase64String(key)))
            {
                var encrypt = rijndaelHelper.Encrypt(RawValue);
                var decrypt = rijndaelHelper.Decrypt(encrypt);
                Assert.AreEqual(RawValue, decrypt);
            }
        }

        [Test]
        public void CanEncrypt()
        {
            //These two values should not be hard coded in your code.
            byte[] key = { 251, 9, 67, 117, 237, 158, 138, 150, 255, 97, 103, 128, 183, 65, 76, 161, 7, 79, 244, 225, 146, 180, 51, 123, 118, 167, 45, 10, 184, 181, 202, 190 };
            
            using (var rijndaelHelper = new AESRandomCryptoProvider(Convert.ToBase64String(key)))
            {
                var encrypt = rijndaelHelper.Encrypt(RawValue);
                Assert.AreNotEqual("StringToEncrypt", encrypt);
            }
        }
    }
}