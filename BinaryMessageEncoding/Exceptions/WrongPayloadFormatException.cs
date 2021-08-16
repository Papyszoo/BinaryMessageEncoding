using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding.Exceptions
{
    public class WrongPayloadFormatException : Exception
    {
        public WrongPayloadFormatException(string message) : base(message) {}
    }
}
