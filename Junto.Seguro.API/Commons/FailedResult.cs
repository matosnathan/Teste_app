using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Junto.Seguros.Domain.Commons;

namespace Junto.Seguros.API.Commons
{
    public class FailedResult
    {
        public string Message { get; set; }
        public List<DomainValidation> Errors { get; set; }

        public FailedResult(string message, List<DomainValidation> validations)
        {
            Message = message;
            Errors = validations;
        }
    }
}
