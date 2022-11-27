using DOCSAN.APPLICATION.Handlers.Users.Queries;
using FluentValidation;

namespace DOCSAN.APPLICATION.Handlers.Users.Validators
{
    public class UserDetailQueryValidator : AbstractValidator<UserDetailQuery>
    {
        public UserDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull().WithMessage("Id can not be null or empty");
        }
    }
}
