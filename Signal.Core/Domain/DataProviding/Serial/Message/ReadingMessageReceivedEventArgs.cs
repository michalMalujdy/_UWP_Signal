using System;
using System.Collections.Generic;

namespace Signal.Core.Domain.DataProviding.Serial.Message
{
    public class ReadingMessageReceivedEventArgs : EventArgs
    {
        public ICollection<ReadingsMessage> ReadingsMessages { get; set; }
    }
}