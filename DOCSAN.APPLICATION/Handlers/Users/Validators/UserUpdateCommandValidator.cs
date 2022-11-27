using DOCSAN.APPLICATION.Handlers.Users.Commands;
using FluentValidation;

namespace DOCSAN.APPLICATION.Handlers.Users.Validators
{
    public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
    {
        public UserUpdateCommandValidator()
        {
            RuleFor(v => v.Mail).NotEmpty().NotNull().WithMessage("Mail can not be null or empty.");
            RuleFor(v => v.Mail).EmailAddress().MinimumLength(7).MaximumLength(55).WithMessage("Mail address must contain @ and minimum 7 max 55 characters");
            RuleFor(v => v.Role).NotNull().NotEmpty().NotEqual(SHARED.Enums.enmRole.Undefined).WithMessage("Role can not be null");
        }
    }
}
