using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding.Exceptions
{
    public class PayloadTooBigException : Exception
    {
        public PayloadTooBigException(string message) : base(message) {}
    }
}
