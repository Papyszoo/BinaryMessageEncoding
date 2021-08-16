using BinaryMessageEncoding.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryMessageEncoding
{
    public static class ValidationHelper
    {
        public static void ValidateMessage(Message message)
        {
            checkHeaderCount(message.headers.Count);
            checkHeadersSize(message.headers);
            checkPayloadSize(message.payload);
        }

        private static void checkHeaderCount(int count)
        {
            if (count > 63)
            {
                throw new TooManyHeadersException($"Too many headers: {count}. You can provide up to 63 headers");
            }
        }

        private static void checkHeadersSize(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                if (header.Key.Length > 1023 || header.Value.Length > 1023)
                {
                    throw new HeaderTooBigException($"Header is too big: {header.Key} = {header.Value}. Maximum header size is 1023 bytes.");
                }
            }
        }

        private static void checkPayloadSize(byte[] payload)
        {
            if (payload.Length > (256 * 1024))
            {
                throw new PayloadTooBigException($"Payload size is: {payload.Length}. Maximum size is 256KiB");
            }
        }
    }
}
