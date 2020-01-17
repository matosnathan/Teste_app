using System.Threading.Tasks;

namespace Junto.Seguros.Domain.Commons
{
    public interface IServiceBase<T,TId, TCreateCommand, TUpdateCommand, TDto>
        where T : DomainBase<TId>
        where TCreateCommand : TCommand
        where TUpdateCommand : TCommand
    {

        Task<TDto> GetAsync(TId id);

        Task<TDto> PostAsync(TCreateCommand command);

        Task<TDto> UpdateAsync(TUpdateCommand command);


    }

    public interface IServiceBase<T, TCreateCommand, TUpdateCommand, TDto>: IServiceBase<T,long,TCreateCommand,TUpdateCommand,TDto>
        where T : DomainBase
        where TCreateCommand : TCommand
        where TUpdateCommand : TCommand
    {

    }
}
