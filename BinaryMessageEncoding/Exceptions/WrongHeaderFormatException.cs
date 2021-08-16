using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding.Exceptions
{
    public class WrongHeaderFormatException : Exception
    {
        public WrongHeaderFormatException(string message) : base(message) {}
    }
}
