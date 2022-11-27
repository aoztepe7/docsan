using DOCSAN.APPLICATION.Handlers.Projects.Commands;
using FluentValidation;

namespace DOCSAN.APPLICATION.Handlers.Projects.Validators
{
    public class ProjectUpdateCommandValidator : AbstractValidator<ProjectUpdateCommand>
    {
        public ProjectUpdateCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty().NotNull().WithMessage("Name can not be null or empty.");
        }
    }
}
