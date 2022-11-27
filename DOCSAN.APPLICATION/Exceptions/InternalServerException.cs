using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Exceptions
{
    public class InternalServerException : Exception
    {
        private string? _message { get; set; }
        public InternalServerException() : base()
        {
        }

        public InternalServerException(string message) : base(message)
        {
            _message = message;
        }
    }
}
