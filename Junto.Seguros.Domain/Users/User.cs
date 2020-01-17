using System;
using Junto.Seguros.Domain.Commons;
using Junto.Seguros.Domain.Users.Validations;

namespace Junto.Seguros.Domain.Users
{
    public class User : DomainBase
    {
        public string Login { get; protected set; }
        public string Name { get; protected set; }
        public string HashPassword { get; protected set; }
        public string Salt { get; protected set; }
        public string Email { get; protected set; }

        protected User()
        {

        }

        public User(string login, string name, string email)
        {
            Login = login;
            Name = name;
            Email = email;
            GenerateSalt();
        }

        public User CreatePassword(string password, IEncrypterService encrypter)
        {
            if (password.PasswordIsValid())
            {
                HashPassword = encrypter.Encrypt(password, Salt);
            }

            return this;

        }

        public User ChangePassword(string newPassword, string oldPassword, IEncrypterService encrypter)
        {
            var hashOldPassword = encrypter.Encrypt(oldPassword, Salt);
            
            if (HashPassword == hashOldPassword && !encrypter.Compare(newPassword, Salt, HashPassword))
            {
                if (newPassword.PasswordIsValid())
                {
                    GenerateSalt();
                    HashPassword = encrypter.Encrypt(newPassword, Salt);
                }
            }


            return this;
        }

        protected User GenerateSalt()
        {
            Salt = Guid.NewGuid().ToString();
            return this;
        }

        public User Delete()
        {
            IsDeleted = true;
            return this;
        }

        public User ChangeName(string newName)
        {
            if (newName.NameIsValid())
                Name = newName;

            return this;
        }

        public User ChangeEmail(string email)
        {
            if (email.EmailIsValid())
                Email = email;

            return this;
        }

        public bool IsValid()
        {
            return this.UserIsValid();
        }

    }
}
