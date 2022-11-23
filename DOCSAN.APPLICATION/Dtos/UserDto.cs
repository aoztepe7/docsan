using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Enums;

namespace DOCSAN.APPLICATION.Dtos
{
    public class UserDto : BaseDto<UserDto, User>
    {
        public Guid Id { get; set; }
        public string Mail { get; set; }
        public enmRole Role { get; set; }
        public bool Status { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }    
        public string? LastModifiedBy { get; set; }
      
    }
}
