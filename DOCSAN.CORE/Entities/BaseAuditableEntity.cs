using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.CORE.Entities
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
