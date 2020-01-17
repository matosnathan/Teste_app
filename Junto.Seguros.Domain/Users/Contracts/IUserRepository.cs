using System;
using System.Collections.Generic;
using System.Text;
using Junto.Seguros.Domain.Commons;

namespace Junto.Seguros.Domain.Users.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetByLogin(string login);

    }
}
