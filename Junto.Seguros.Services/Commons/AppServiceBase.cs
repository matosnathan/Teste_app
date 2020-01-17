using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Junto.Seguros.Domain.Commons;

namespace Junto.Seguros.Services.Commons
{
    public class AppServiceBase<T,TId,TCreateCommand,TUpdateCommand,TDto> : IServiceBase<T,TId,TCreateCommand,TUpdateCommand,TDto> 
    where T : DomainBase<TId> 
    where TCreateCommand:TCommand
    where TUpdateCommand:TCommand
    {
        protected IRepositoryBase<T,TId> _repository;
        protected IMapper _mapper;
        protected IDomainNotificationProvider _notificationProvider;
        
        public AppServiceBase(IRepositoryBase<T,TId> repository, IMapper mapper, IDomainNotificationProvider notificationProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _notificationProvider = notificationProvider;
        }

        public virtual async Task<TDto> GetAsync(TId id)
        {
            var entity = _repository.Get(id);

            if(entity == null)
            { 
                _notificationProvider.AddValidationError("User","Usuário não encontrado");
            }

            return _mapper.Map<T, TDto>(entity);
        }

        public virtual async Task<TDto> PostAsync(TCreateCommand command)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<TDto> UpdateAsync(TUpdateCommand command)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        protected bool CheckRules<TValidation,TEntity>(TEntity entity) where TValidation : AbstractValidator<TEntity>
        {
            var errors = ((TValidation) Activator.CreateInstance(typeof(TValidation))).Validate(entity).Errors;

            foreach (var error in errors)
            {
                _notificationProvider.AddValidationError(error.PropertyName,error.ErrorMessage);
            }

            return !errors.Any();
        }

        protected async Task CommitAsync()
        {
            if (!_notificationProvider.HasErrors())
                await _repository.CommitAsync();
        }
    }

    public class AppServiceBase<T, TCreateCommand, TUpdateCommand, TDto> : AppServiceBase<T, long, TCreateCommand, TUpdateCommand
            , TDto>
        where T : DomainBase
        where TCreateCommand : TCommand
        where TUpdateCommand : TCommand
    {
        public AppServiceBase(IRepositoryBase<T, long> repository, IMapper mapper, IDomainNotificationProvider notificationProvider) : base(repository, mapper, notificationProvider)
        {
        }
    }
}
