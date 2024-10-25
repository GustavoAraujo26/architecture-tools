using ArchitectureTools.Security;

namespace ArchitectureTools.Tests.FactoryTests
{
    public class CryptographyFactoryTests
    {
        [Fact]
        public void ShouldReturnSuccess_WhenCryptographText()
        {
            string password = "Cryptograph@123";
            string key = Guid.NewGuid().ToString();

            var encryptedPassword = CryptographyFactory.EncryptData(password, key);
            var decryptedPassword = CryptographyFactory.DecryptData(encryptedPassword, key);
            var encryptedDecryptedPassword = CryptographyFactory.EncryptData(decryptedPassword, key);

            Assert.Equal(password, decryptedPassword);
            Assert.Equal(encryptedPassword, encryptedDecryptedPassword);
        }
    }
}
