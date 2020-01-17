using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Junto.Seguros.Domain.Commons;

namespace Junto.Seguros.Infra.Encrypters
{
    public class EncrypterService : IEncrypterService
    {
        private static readonly ThreadLocal<StringBuilder> CachedBuilder =
            new ThreadLocal<StringBuilder>(() => new StringBuilder());
        public string Encrypt(string value, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var data = sha256.ComputeHash(Encoding.UTF8.GetBytes(value+salt));

                var sBuilder = CachedBuilder.Value;
                sBuilder.Length = 0;

                foreach (var b in data)
                {
                    sBuilder.Append(b.ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public bool Compare(string value, string salt, string hash)
        {
            var hashOfInput = Encrypt(value, salt);

            return 0 == StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hash);
        }
    }
}
