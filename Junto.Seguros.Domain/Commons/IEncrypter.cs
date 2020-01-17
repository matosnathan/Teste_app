using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Commons
{
    public interface IEncrypter
    {
        string Encrypt(string value, string hash);
    }
}
