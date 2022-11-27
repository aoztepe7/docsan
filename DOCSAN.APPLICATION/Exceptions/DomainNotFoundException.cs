using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Exceptions
{
    public class DomainNotFoundException : Exception
    {
        public string ClassName { get; }
        public DomainNotFoundException() : base("Not Found")
        {
            ClassName = String.Empty;
        }

        public DomainNotFoundException(string domain) : this()
        {
            ClassName = domain;
        }
    }
}
