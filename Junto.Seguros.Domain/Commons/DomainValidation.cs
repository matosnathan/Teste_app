using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Commons
{
    public class DomainValidation
    {
        public string Key { get; set; }
        public string Message { get; set; }

        public DomainValidation(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public DomainValidation(string message)
        {
            Key = "";
            Message = message;
        }
    }
}
