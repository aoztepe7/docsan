using DOCSAN.SHARED.Enums;

namespace DOCSAN.CORE.Entities
{
    public class User : BaseAuditableEntity
    {
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public enmGender Gender { get; set; } = enmGender.Undefined;
        public enmRole Role { get; set; } = enmRole.User;
        public string ImageUrl { get; set; }
    }
}
