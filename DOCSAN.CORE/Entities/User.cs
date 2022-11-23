using DOCSAN.SHARED.Enums;

namespace DOCSAN.CORE.Entities
{
    public class User : BaseAuditableEntity
    {
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public enmRole Role { get; set; }
    }
}
