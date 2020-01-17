using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Auths
{
    public class AuthDto
    {
        public string Token { get; set; }

        public AuthDto(string token)
        {
            Token = token;
        }
    }
}
