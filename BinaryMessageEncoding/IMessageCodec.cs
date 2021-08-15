using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding
{
    public interface IMessageCodec
    {
        byte[] Encode(Message message);
        Message Decode(byte[] data);
    }
}
