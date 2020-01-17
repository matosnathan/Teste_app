using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Junto.Seguros.Domain.Commons;
using Junto.Seguros.Domain.Users;
using Junto.Seguros.Domain.Users.Commands;
using Junto.Seguros.Domain.Users.Contracts;
using Junto.Seguros.Domain.Users.Validations;
using Junto.Seguros.Services.Commons;

namespace Junto.Seguros.Services.Users
{
    public class UserAppService : AppServiceBase<User, UserCreateCommand, UserUpdateCommand, UserDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDomainNotificationProvider _notificationProvider;
        private readonly IEncrypterService _encrypterService;
        public UserAppService(IUserRepository repository, IMapper mapper, IDomainNotificationProvider notificationProvider, IEncrypterService encrypterService) : base(repository, mapper,notificationProvider)
        {
            _userRepository = repository;
            _notificationProvider = notificationProvider;
            _encrypterService = encrypterService;
        }

        public override async Task<UserDto> PostAsync(UserCreateCommand command)
        {
            if (!CheckRules<UserRegisterCommandValidation, UserCreateCommand>(command))
                return null;
            
            //criar metodo repository de buscar por login
            if (_userRepository.GetByLogin(command.Login) != null)
                _notificationProvider.AddValidationError("Login", "Já existe um usuário com este login");


            if (_notificationProvider.HasErrors())
                return null;


            //valida os dados
            //encriptar password
            var user = new User(command.Login, command.Name,command.Email)
                .CreatePassword(command.Password,_encrypterService);

            if (CheckRules<UserValidation, User>(user))
                await _repository.InsertAsync(user);

            await CommitAsync();


            return _mapper.Map<UserDto>(user);
        }

        public override async Task<UserDto> UpdateAsync(UserUpdateCommand command)
        {
            var user = _repository.Get(command.Id);

            if (user == null)
            {
                _notificationProvider.AddValidationError("Id", "Usuário não encontrado");
                return null;
            }

            if (!CheckRules<UserUpdateCommandValidation, UserUpdateCommand>(command))
                return null;

            user
                .ChangeName(command.Name)
                .ChangeEmail(command.Email);

            await _repository.UpdateAsync(user);

            await CommitAsync();

            return _mapper.Map<UserDto>(user);


        }

        public override async Task DeleteAsync(long id)
        {
            var user = _repository.Get(id);

            if(user == null)
                _notificationProvider.AddValidationError("User","Usuário não encontrado");

            user.Delete();

            await _repository.UpdateAsync(user);

            await CommitAsync();
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = _repository.List(x => !x.IsDeleted);

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task ChangePassword(UserChangePasswordCommand command)
        {
            var user = _userRepository.Get(command.Id);

            if (user == null)
            {
                _notificationProvider.AddValidationError("User","Usuário não encontrado");
                return;
            }

            if (!CheckRules<UserChangePasswordValidation, UserChangePasswordCommand>(command))
                return;

            user.ChangePassword(command.NewPassword, command.OldPassword,_encrypterService);

            if (CheckRules<UserValidation, User>(user))
            {
                await _userRepository.UpdateAsync(user);

                await CommitAsync();
            }
            
        }
    }
}
