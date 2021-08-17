using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System;
using BusinessLayer.Helpers.Interfaces;
using BusinessLayer.Models;

namespace BusinessLayer.Services
{
    public class HashService : IHashService
    {
        private readonly HashSettings _hashSettings;

        public HashService(IOptions<HashSettings> hashSettings)
        {
            _hashSettings = hashSettings.Value;
        }

        public string HashString(string stringToHash)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: stringToHash,
                salt: Convert.FromBase64String(_hashSettings.PasswordSalt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: _hashSettings.IterationCount,
                numBytesRequested: _hashSettings.NumberBytesRequested));
        }
    }
}