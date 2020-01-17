using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Users.Commands
{
    public class UserChangePasswordCommand
    {
        public long Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
