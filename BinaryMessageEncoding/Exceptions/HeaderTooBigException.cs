using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding.Exceptions
{
    public class HeaderTooBigException : Exception
    {
        public HeaderTooBigException(string message) : base(message) {}
    }
}
