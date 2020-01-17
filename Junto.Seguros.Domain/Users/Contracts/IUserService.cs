using System.Collections.Generic;
using System.Threading.Tasks;
using Junto.Seguros.Domain.Commons;
using Junto.Seguros.Domain.Users.Commands;

namespace Junto.Seguros.Domain.Users.Contracts
{
    public interface IUserService : IServiceBase<User,UserCreateCommand,UserUpdateCommand,UserDto>
    {
        Task<List<UserDto>> GetAll();
        Task ChangePassword(UserChangePasswordCommand command);
        Task DeleteAsync(long id);
    }
}
