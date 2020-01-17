using Junto.Seguros.Domain.Commons;

namespace Junto.Seguros.Domain.Users.Commands
{
    public class UserCreateCommand : TCommand
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

    }
}
