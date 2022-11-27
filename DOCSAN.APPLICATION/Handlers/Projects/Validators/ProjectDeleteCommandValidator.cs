using DOCSAN.APPLICATION.Handlers.Projects.Commands;
using FluentValidation;

namespace DOCSAN.APPLICATION.Handlers.Projects.Validators
{
    public class ProjectDeleteCommandValidator : AbstractValidator<ProjectDeleteCommand>
    {
        public ProjectDeleteCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull().WithMessage("Id can not be null or empty");
        }
    }
}
