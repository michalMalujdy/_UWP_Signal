using System.Collections.Generic;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Services;

namespace Signal.Core.Domain
{
    public class RecordingSession
    {
        public bool IsRunning { get; private set; }
        public ICollection<ReadingsMessage> ReadingsMessages { get; private set; } 
            = new List<ReadingsMessage>();

        private readonly Serial _serial;

        public RecordingSession(Serial serial)
        {
            _serial = serial;
        }
        
        public void Start(string portName)
        {
            if (IsRunning)
                return;
            
            IsRunning = true;
            _serial.DataProvider.Open(portName);
            _serial.DataProvider.DataReceived += DataReceivedHandler;
        }

        public void StopAndSave(string filename)
        {
            // TODO Write ReadingsMessages to csv file
            
            ReadingsMessages = new List<ReadingsMessage>();
            IsRunning = false;
        }

        private void DataReceivedHandler(object sender, ReadingMessageReceivedEventArgs e)
        {
            ReadingsMessages.Add(e.ReadingsMessage);
        }
    }
}