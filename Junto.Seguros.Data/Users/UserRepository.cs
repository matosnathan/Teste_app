using System;
using System.Collections.Generic;
using System.Text;
using Junto.Seguros.Data.Commons;
using Junto.Seguros.Data.Context;
using Junto.Seguros.Domain.Users;
using Junto.Seguros.Domain.Users.Contracts;

namespace Junto.Seguros.Data.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DataContext db) : base(db)
        {
        }

        public User GetByLogin(string login)
        {
            return Get(x => x.Login == login);
        }
    }
}
