using System;
using System.Collections.Generic;
using System.Text;
using Junto.Seguros.Domain.Users;
using Junto.Seguros.Domain.Users.Commands;
using Junto.Seguros.Services.Commons;

namespace Junto.Seguros.Services.Users
{
    public class UserMapper : MapperBase
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>();
            
        }
    }
}
