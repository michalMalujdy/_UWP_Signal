using System;

namespace Signal.Core.Domain.DataProviding
{
    public class ReadingMessageReceivedEventArgs : EventArgs
    {
        public ReadingsMessage ReadingsMessage { get; set; }
    }
}