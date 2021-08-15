using System;
using System.Collections.Generic;

namespace BinaryMessageEncoding
{
    public class Message
    {
        public Dictionary<string, string> headers;
        public byte[] payload;
    }
}
