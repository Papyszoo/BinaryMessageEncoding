using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding.Exceptions
{
    public class NoPayloadWasSentException : Exception
    {
        public NoPayloadWasSentException(string message) : base(message) {}
    }
}
