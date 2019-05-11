using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Signal.Core.Domain.DataProviding.Serial.Message;

namespace Signal.Core.Domain.DataProviding.Serial.SerialTransmission
{
    public class SerialTransmission
    {
        public event EventHandler<FullMessageReceivedEventArgs> FullMessagesReceived;
        public const string MessageEndSign = @"!%";
        public StringBuilder TempContent { get; private set; } = new StringBuilder();

        public void ReadNext(string messagePart)
        {
            TempContent.Append(messagePart);
            var tempContentString = TempContent.ToString();

            if (!tempContentString.Contains(MessageEndSign))
                return;
            
            ReadMessages(messagePart, tempContentString);
        }

        private void ReadMessages(string messagePart, string tempContentString)
        {
            var splitMessages = tempContentString
                .Split(new [] { MessageEndSign }, StringSplitOptions.None);

            TempContent = new StringBuilder(splitMessages.Last());
            
            var readingsMessages = TryDeserializeMessages(splitMessages);
            FullMessagesReceived?.Invoke(this, new FullMessageReceivedEventArgs()
            {
                ReadingsMessages = readingsMessages
            });
        }

        private ReadingsMessage TryDeserializeMessage(string message)
        {
            try
            {
                return JsonConvert.DeserializeObject<ReadingsMessage>(message);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private ICollection<ReadingsMessage> TryDeserializeMessages(IEnumerable<string> messages)
        {
            var readingsMessages = new List<ReadingsMessage>();

            foreach (var message in messages)
            {
                var readingsMessage = TryDeserializeMessage(message);
                
                if (readingsMessage != null)
                    readingsMessages.Add(readingsMessage);
            }

            return readingsMessages;
        }
    }
}