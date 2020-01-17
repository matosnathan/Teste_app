using System;
using System.Collections.Generic;
using System.Text;

namespace Junto.Seguros.Domain.Commons
{
    public class DomainBase<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public DomainBase()
        {
            CreatedAt = DateTime.Now;
            IsDeleted = false;
        }
    }

    public class DomainBase : DomainBase<long>
    {

    }
}
