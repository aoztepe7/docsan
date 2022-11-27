using DOCSAN.CORE.Entities;
using DOCSAN.SHARED.Utils;
using System.Linq.Expressions;

namespace DOCSAN.APPLICATION.Specifications
{
    public class ProjectSpecifications
    {

        public class NameSpecifiation : Specification<Project>
        {
            private readonly string _name;

            public NameSpecifiation(string name)
            {
                _name = name;
            }

            public override Expression<Func<Project, bool>> ToExpression() => project => project.Name != null && project.Name.Contains(_name);
        }
    }
}
