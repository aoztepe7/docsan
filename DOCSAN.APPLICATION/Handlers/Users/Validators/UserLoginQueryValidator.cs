using DOCSAN.APPLICATION.Handlers.Users.Queries;
using FluentValidation;

namespace DOCSAN.APPLICATION.Handlers.Users.Validators
{
    public class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(v => v.Mail).NotEmpty().NotNull().WithMessage("Mail can not be null or empty.");
            RuleFor(v => v.Mail).EmailAddress().MinimumLength(7).MaximumLength(55).WithMessage("Mail address must contain @ and minimum 7 max 55 characters");
            RuleFor(v => v.Password).NotNull().NotEmpty().WithMessage("Password can not be null or empty");
            RuleFor(v => v.Password).MinimumLength(8).WithMessage("Password must contains at least 8 characters");
        }
    }
}
