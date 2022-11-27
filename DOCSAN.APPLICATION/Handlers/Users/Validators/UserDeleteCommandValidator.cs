using DOCSAN.APPLICATION.Handlers.Users.Commands;
using FluentValidation;

namespace DOCSAN.APPLICATION.Handlers.Users.Validators
{
    public class UserDeleteCommandValidator : AbstractValidator<UserDeleteCommand>
    {
        public UserDeleteCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull().WithMessage("Id can not be null or empty");
        }
    }
}
