using System;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Domain.DataProviding.Serial.Message;

namespace Signal.Core.Services
{
    public interface ISerialDataProvider
    {
        event EventHandler<ReadingMessageReceivedEventArgs> DataReceived;
        
        void Open(string portName);
        void Close();
    }
}