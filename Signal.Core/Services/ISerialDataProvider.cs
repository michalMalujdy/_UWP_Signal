using System;
using Signal.Core.Domain.DataProviding;

namespace Signal.Core.Services
{
    public interface ISerialDataProvider
    {
        event EventHandler<ReadingMessageReceivedEventArgs> DataReceived;
        
        void Open(string portName);
        void Close();
    }
}