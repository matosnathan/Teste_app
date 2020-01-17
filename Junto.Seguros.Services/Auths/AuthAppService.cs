using System;
using System.Collections.Generic;
using System.Text;
using Junto.Seguros.Domain.Auths;
using Junto.Seguros.Domain.Auths.Contracts;
using Junto.Seguros.Domain.Commons;
using Junto.Seguros.Domain.Users.Contracts;
using Junto.Seguros.Services.Commons;

namespace Junto.Seguros.Services.Auths
{
    public class AuthAppService : IAuthService
    {
        private readonly IDomainNotificationProvider _notificationProvider;
        private readonly IUserRepository _userRepository;
        private readonly IEncrypterService _encrypterService;
        private readonly IJwtHandler _jwt;
        public AuthAppService(IDomainNotificationProvider notificationProvider, IUserRepository userRepository, IEncrypterService encrypterService, IJwtHandler jwt)
        {
            _notificationProvider = notificationProvider;
            _userRepository = userRepository;
            _encrypterService = encrypterService;
            _jwt = jwt;
        }
        public AuthDto Login(AuthCommand command)
        {
            
            var user = _userRepository.GetByLogin(command.Login);

            if (user == null)
            {
                _notificationProvider.AddValidationError("User","Usuário ou senha inválido");
                return null;
            }

            if (!_encrypterService.Compare(command.Password, user.Salt, user.HashPassword))
            {
                _notificationProvider.AddValidationError("User", "Usuário ou senha inválido");
                return null;
            }

            if (user.IsDeleted)
            {
                _notificationProvider.AddValidationError("User","Usuário inválido");
                return null;
            }

            var jwtToken = _jwt.Create(user.Login, user.Name);

            return new AuthDto(jwtToken.Token);

        }

        
    }
}
