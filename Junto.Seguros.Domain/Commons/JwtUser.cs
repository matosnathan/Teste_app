using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Commons
{
    public class JwtUser
    {
        public string Login { get; set; }
        public string Name { get; set; }

        public JwtUser(string login, string name)
        {
            Login = login;
            Name = name;
        }
    }
}
