using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Commons
{
    public interface IEncrypterService
    {
        string Encrypt(string value, string salt);
        bool Compare(string value, string salt, string hash);
    }
}
