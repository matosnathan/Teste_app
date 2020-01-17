using Junto.Seguros.Domain.Commons;

namespace Junto.Seguros.Domain.Users.Commands
{
    public class UserUpdateCommand : TCommand
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

    }
}
