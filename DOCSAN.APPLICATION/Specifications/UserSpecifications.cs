using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Enums;
using DOCSAN.SHARED.Utils;
using System.Linq.Expressions;

namespace DOCSAN.APPLICATION.Specifications
{
    public class UserSpecifications
    {
      
        public class MailSpecifiation : Specification<User>
        {
            private readonly string _mail;

            public MailSpecifiation(string mail)
            {
                _mail = mail;
            }

            public override Expression<Func<User, bool>> ToExpression() => user => user.Mail != null && user.Mail.Contains(_mail);
        }

        public class FirstNameSpecifiation : Specification<User>
        {
            private readonly string _firstName;

            public FirstNameSpecifiation(string firstName)
            {
                _firstName = firstName;
            }

            public override Expression<Func<User, bool>> ToExpression() => user => user.FirstName != null && user.FirstName.Contains(_firstName);
        }

        public class LastNameSpecifiation : Specification<User>
        {
            private readonly string _lastName;

            public LastNameSpecifiation(string lastName)
            {
                _lastName = lastName;
            }

            public override Expression<Func<User, bool>> ToExpression() => user => user.LastName != null && user.LastName.Contains(_lastName);
        }

        public class GenderSpecifiation : Specification<User>
        {
            private readonly enmGender _gender;

            public GenderSpecifiation(enmGender gender)
            {
                _gender = gender;
            }

            public override Expression<Func<User, bool>> ToExpression() => user => user.Gender != null && user.Gender == _gender;
        }

        public class RoleSpecifiation : Specification<User>
        {
            private readonly enmRole _role;

            public RoleSpecifiation(enmRole role)
            {
                _role = role;
            }

            public override Expression<Func<User, bool>> ToExpression() => user => user.Role == _role;
        }
    }
}
