using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice.Framework.Domain.Exceptions
{
    public class InvalidValueObjectStateException : DomainStateException
    {
        public InvalidValueObjectStateException(string message, params string[] parameters) : base(message)
        {
            Parameters = parameters;
        }
    }
}
