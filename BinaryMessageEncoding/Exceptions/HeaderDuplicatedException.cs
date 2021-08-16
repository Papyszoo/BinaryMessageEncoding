using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding.Exceptions
{
    public class HeaderDuplicatedException : Exception
    {
        public HeaderDuplicatedException(string message) : base(message) {}
    }
}
