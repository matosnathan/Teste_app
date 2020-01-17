using System.Collections.Generic;

namespace Junto.Seguros.Domain.Commons
{
    public interface IDomainNotificationProvider
    {
        bool HasErrors();

        void AddValidationError(string key, string message);

        void AddValidationError(DomainValidation validation);

        List<DomainValidation> GetErrors();
    }
}
