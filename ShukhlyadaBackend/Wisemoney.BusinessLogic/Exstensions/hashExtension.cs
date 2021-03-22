using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Shukhlyada.BusinessLogic.Exstensions
{
    public static class hashExtension
    {
        public static string GenerateSalt()
        {
            byte[] byteSalt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteSalt);
            }

            return Convert.ToBase64String(byteSalt);
        }

        public static string SHA2Hash(this string password, string salt)
        {
            var byteSalt = Convert.FromBase64String(salt);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: byteSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
