using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentValidation;
using FluentValidation.Validators;
using Junto.Seguros.Domain.Commons;
using Junto.Seguros.Domain.Users.Commands;

namespace Junto.Seguros.Domain.Users.Validations
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(x => x.Login)
                .SetValidator(new LoginValidation());

            RuleFor(x => x.Name)
                .SetValidator(new NameValidation());

            RuleFor(x => x.Email)
                .SetValidator(new EmailValidator());

            RuleFor(x => x.Salt)
                .NotEmpty().WithMessage("Salt do usuário não foi gerado");

            RuleFor(x => x.HashPassword)
                .NotEmpty().WithMessage("Senha não cadastrada");



        }
    }

    public class UserRegisterCommandValidation : AbstractValidator<UserCreateCommand>
    {
        public UserRegisterCommandValidation()
        {
            RuleFor(x=>x.Login)
                .SetValidator(new LoginValidation());

            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidation());

            RuleFor(x => x.Name)
                .SetValidator(new NameValidation());


            RuleFor(x => x.Email)
                .SetValidator(new EmailValidator());



        }
    }

    public class UserChangePasswordValidation : AbstractValidator<UserChangePasswordCommand>
    {
        public UserChangePasswordValidation()
        {
            RuleFor(x => x.NewPassword)
                .SetValidator(new PasswordValidation())
                .Equal(x => x.ConfirmPassword).WithMessage("As senhas são diferentes");
        }
    }

    public class UserUpdateCommandValidation : AbstractValidator<UserUpdateCommand>
    {
        public UserUpdateCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(0)
                .WithMessage("Id do usuário deve ser informado");

            RuleFor(x => x.Name)
                .SetValidator(new NameValidation());

            RuleFor(x => x.Email)
                .SetValidator(new EmailValidator());


        }
    }

    public class EmailValidation : AbstractValidator<string>
    {
        public EmailValidation()
        {
            RuleFor(x => x)
                .EmailAddress().WithMessage("Email inválido")
                .NotEmpty().WithMessage("Obrigatório informar um email");
        }
    }

    public class NameValidation : AbstractValidator<string>
    {
        public NameValidation()
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage("Obrigatório informar o nome");
        }
    }

    public class LoginValidation : AbstractValidator<string>
    {
        public LoginValidation()
        {
            RuleFor(x => x)
                .MinimumLength(5).WithMessage("Login deve ter no mínimo 5 caracteres")
                .MaximumLength(20).WithMessage("Login deve ter no máximo 20 caracteres");
        }
    }

    public class PasswordValidation : AbstractValidator<string>
    {
        public PasswordValidation()
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage("Informe uma senha")
                .MinimumLength(8).WithMessage("A senha deve ter no mínimo 8 caracteres");
        }
    }

}
