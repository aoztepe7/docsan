using DOCSAN.CORE.Entities;

namespace DOCSAN.APPLICATION.Dtos
{
    public class ProjectDto : BaseDto<ProjectDto, Project>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
