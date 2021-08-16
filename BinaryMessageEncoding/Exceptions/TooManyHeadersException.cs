using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding.Exceptions
{
    public class TooManyHeadersException : Exception
    {
        public TooManyHeadersException(string message) : base(message) {}
    }
}
