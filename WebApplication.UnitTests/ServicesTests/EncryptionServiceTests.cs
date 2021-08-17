using System;
using BusinessLayer.Services;
using FluentAssertions;
using NUnit.Framework;

namespace WebApplication.UnitTests.ServicesTests
{
    public class EncryptionServiceTests
    {
        private readonly EncryptionService _encryptionService;

        public EncryptionServiceTests()
        {
            _encryptionService = new EncryptionService();
        }

        [TestCase("Test info")]
        [TestCase("Some long long! $!@:L:!@$:K!@%#%9075)I(^)%O^")]
        public void Encrypt_Decrypt_ShouldReturnSameData(string expected)
        {
            var encrypted = _encryptionService.Encrypt(expected);
            var decrypted = _encryptionService.Decrypt(encrypted);

            decrypted.Should().Be(expected);
        }

        [Test]
        public void Encrypt_WhenEmptyStringPassed_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _encryptionService.Encrypt(string.Empty));
        }
    }
}
