using System;
using System.Collections.Generic;
using Signal.Core.Domain.DataProviding.Serial.Message;

namespace Signal.Core.Domain.DataProviding.Serial.SerialTransmission
{
    public class FullMessageReceivedEventArgs : EventArgs
    {
        public ICollection<ReadingsMessage> ReadingsMessages { get; set; }
    }
}