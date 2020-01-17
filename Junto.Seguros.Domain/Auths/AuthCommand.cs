using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Auths
{
    public class AuthCommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
