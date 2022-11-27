using DOCSAN.APPLICATION.Handlers.Projects.Queries;
using FluentValidation;

namespace DOCSAN.APPLICATION.Handlers.Projects.Validators
{
    public class ProjectDetailQueryValidator : AbstractValidator<ProjectDetailQuery>
    {
        public ProjectDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull().WithMessage("Id can not be null or empty");
        }
    }
}
