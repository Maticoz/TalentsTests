using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentsApi.Exceptions
{
    public class InternalErrorException : Exception
    {
        public InternalErrorException(string message) : base(message)
        {

        }
    }
}
