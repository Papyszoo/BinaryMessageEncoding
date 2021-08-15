using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding
{
    public class MessageCodec : IMessageCodec
    {
        public Message Decode(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] Encode(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
