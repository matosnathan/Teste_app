using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Auths.Contracts
{
    public interface IAuthService
    {
        AuthDto Login(AuthCommand command);
    }
}
