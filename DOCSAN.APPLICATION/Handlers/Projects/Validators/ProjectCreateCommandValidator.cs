using DOCSAN.APPLICATION.Handlers.Projects.Commands;
using FluentValidation;

namespace DOCSAN.APPLICATION.Handlers.Projects.Validators
{
    public class ProjectCreateCommandValidator : AbstractValidator<ProjectCreateCommand>
    {
        public ProjectCreateCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty().NotNull().WithMessage("Name can not be null or empty.");
        }
    }
}
