using BinaryMessageEncoding.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryMessageEncoding
{
    public class MessageCodec : IMessageCodec
    {
        public Message Decode(byte[] data)
        {
            Message message = new Message();
            string dataString = Encoding.ASCII.GetString(data);
            var lines = dataString.TrimEnd().Split('\n');

            message.headers = getHeadersFromData(lines);

            //Process payload
            var payloadLine = lines[lines.Length - 1];
            
            message.payload = getPayloadFromData(payloadLine);

            ValidationHelper.ValidateMessage(message);
            return message;
        }

        #region decode methods
        private Dictionary<string, string> getHeadersFromData(string[] lines) 
        {
            var headerCount = lines.Length - 1;
            var headers = new Dictionary<string, string>();
            for (int i = 0; i < headerCount; i++)
            {
                var header = lines[i].Split('=');
                if (header.Length != 2)
                {
                    throw new WrongHeaderFormatException($"Header {lines[i]} is not formatted correctly.");
                }
                if (headers.ContainsKey(header[0]))
                {
                    throw new HeaderDuplicatedException($"Header with a key {header[0]} was sent at least twice.");
                }
                headers.Add(header[0], header[1]);
            }
            return headers;
        }

        private byte[] getPayloadFromData(string line)
        {
            var payload = line.Split('=');
            if (payload.Length != 2)
            {
                throw new WrongPayloadFormatException($"Payload {line} is not formatted correctly.");
            }
            if(payload[0] != "Payload")
            {
                throw new NoPayloadWasSentException("No Payload Was Sent");
            }
            return Encoding.ASCII.GetBytes(payload[1]);

        }
        #endregion

        public byte[] Encode(Message message)
        {
            var sb = new StringBuilder();
            //Append headers
            foreach (var item in message.headers)
            {
                sb.Append($"{item.Key}={item.Value}\n");
            }
            //Append payload
            sb.Append("Payload=");
            byte[] bytes = Encoding.ASCII.GetBytes(sb.ToString());
            return bytes.Concat(message.payload).ToArray();
        }
    }
}
