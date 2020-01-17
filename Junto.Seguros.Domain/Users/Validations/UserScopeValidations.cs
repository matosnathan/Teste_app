using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Users.Validations
{
    public static class UserScopeValidations
    {
        public static bool EmailIsValid(this string email)
        {
            return new EmailValidation().Validate(email).IsValid;
        }

        public static bool NameIsValid(this string name)
        {
            return new NameValidation().Validate(name).IsValid;
        }

        public static bool PasswordIsValid(this string password)
        {
            return new PasswordValidation().Validate(password).IsValid;
        }

        public static bool UserIsValid(this User user)
        {
            return new UserValidation().Validate(user).IsValid;
        }

    }
}
