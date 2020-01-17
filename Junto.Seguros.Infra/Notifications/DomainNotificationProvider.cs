using Junto.Seguros.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Junto.Seguros.Infra.Notifications
{
    public class DomainNotificationProvider : IDomainNotificationProvider
    {
        private List<DomainValidation> _validations = new List<DomainValidation>();
        public bool HasErrors()
        {
            return _validations.Any();
        }

        public void AddValidationError(string key, string message)
        {
            _validations.Add(new DomainValidation(key,message));
        }

        public void AddValidationError(DomainValidation validation)
        {
            _validations.Add(validation);
        }

        public List<DomainValidation> GetErrors()
        {
            return _validations;
        }
    }
}
