using BinaryMessageEncoding.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BinaryMessageEncoding.Test
{
    public class MessageTest
    {
        [Fact]
        public void Decode_ShouldParseSimpleMessage()
        {
            //Arrange
            string messageString = "Test=test\nTest2=test2\nTest3=test3\nPayload=payloadtest";
            byte[] bytes = Encoding.ASCII.GetBytes(messageString);

            byte[] payload = Encoding.ASCII.GetBytes("payloadtest");
            IMessageCodec codec = new MessageCodec();

            //Act
            Message message = codec.Decode(bytes);

            //Assert
            Assert.True(message.headers.ContainsKey("Test"));
            Assert.True(message.headers.ContainsKey("Test2"));
            Assert.True(message.headers.ContainsKey("Test3"));

            Assert.Equal(payload, message.payload);
        }

        [Fact]
        public void Encode_ShouldBuildMessage()
        {
            //Arrange
            byte[] payload = Encoding.ASCII.GetBytes("payloadtest");
            Message message = new Message()
            {
                headers = new Dictionary<string, string>()
                {
                    { "Test", "test" },
                    { "Test2", "test2" },
                    { "Test3", "test3" }
                },
                payload = payload
            };
            IMessageCodec codec = new MessageCodec();
            //Act
            byte[] data = codec.Encode(message);
            string dataString = Encoding.ASCII.GetString(data);

            //Assert
            Assert.Contains("Test=test", dataString);
            Assert.Contains("Test2=test2", dataString);
            Assert.Contains("Test3=test3", dataString);
            Assert.Contains("Payload=payloadtest", dataString);
        }

        [Fact]
        public void Decode_ShouldThrowHeaderTooBigException()
        {
            //Arrange
            string messageString = new string('x', 5000) + "Test=test\nTest2=test2\nTest3=test3\nPayload=payloadtest";
            byte[] bytes = Encoding.ASCII.GetBytes(messageString);

            byte[] payload = Encoding.ASCII.GetBytes("payloadtest");
            IMessageCodec codec = new MessageCodec();

            //Assert
            Assert.Throws<HeaderTooBigException>(() => codec.Decode(bytes));
        }

        [Fact]
        public void Decode_ShouldThrowHeaderDuplicatedException()
        {
            //Arrange
            string messageString = string.Concat(Enumerable.Repeat("x=y\n", 2)) + "Payload=payloadtest";
            byte[] bytes = Encoding.ASCII.GetBytes(messageString);

            byte[] payload = Encoding.ASCII.GetBytes("payloadtest");
            IMessageCodec codec = new MessageCodec();

            //Assert
            Assert.Throws<HeaderDuplicatedException>(() => codec.Decode(bytes));
        }

        [Fact]
        public void Decode_ShouldThrowTooManyHeadersException()
        {
            //Arrange
            string messageString = String.Empty;
            for (int i = 0; i<64; i++)
            {
                messageString += $"Test{i}=test{i}\n";
            }
            messageString += "Payload=payloadtest";
            byte[] bytes = Encoding.ASCII.GetBytes(messageString);
            IMessageCodec codec = new MessageCodec();

            //Assert
            Assert.Throws<TooManyHeadersException>(() => codec.Decode(bytes));
        }

        [Theory]
        [InlineData("aaa\n")]
        [InlineData("aaa=aaa=aaa\n")]
        public void Decode_ShouldThrowWrongHeaderFormatException(string header)
        {
            //Arrange
            string messageString = header;
            messageString += "Payload=payloadtest";
            byte[] bytes = Encoding.ASCII.GetBytes(messageString);
            IMessageCodec codec = new MessageCodec();

            //Assert
            Assert.Throws<WrongHeaderFormatException>(() => codec.Decode(bytes));
        }

        [Theory]
        [InlineData("aaa")]
        [InlineData("aaa=aaa=aaa")]
        [InlineData("aaa\n")]
        [InlineData("aaa=aaa=aaa\n")]
        public void Decode_ShouldThrowWrongPayloadFormatException(string payload)
        {
            //Arrange
            string messageString = "Test=test\nTest2=test2\nTest3=test3\n";
            messageString += payload;
            byte[] bytes = Encoding.ASCII.GetBytes(messageString);
            IMessageCodec codec = new MessageCodec();

            //Assert
            Assert.Throws<WrongPayloadFormatException>(() => codec.Decode(bytes));
        }

        [Fact]
        public void Decode_ShouldThrowNoPayloadWasSentException()
        {
            //Arrange
            string messageString = "Test=test\nTest2=test2\nTest3=test3\n";
            byte[] bytes = Encoding.ASCII.GetBytes(messageString);
            IMessageCodec codec = new MessageCodec();

            //Assert
            Assert.Throws<NoPayloadWasSentException>(() => codec.Decode(bytes));
        }
    }
}
