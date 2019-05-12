using System;
using System.Collections.Generic;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Domain.DataProviding.Serial;
using Signal.Core.Domain.DataProviding.Serial.Message;
using Signal.Core.Services;

namespace Signal.Core.Domain
{
    public class RecordingSession
    {
        public bool IsRunning { get; private set; }
        public ICollection<ReadingsMessage> ReadingsMessages { get; private set; } 
            = new List<ReadingsMessage>();

        private readonly Serial _serial;
        private readonly IReadingsSaver _readingsSaver;

        public RecordingSession(Serial serial, IReadingsSaver readingsSaver)
        {
            _serial = serial;
            _readingsSaver = readingsSaver;
        }
        
        public void Start(string portName)
        {
            if (IsRunning)
                return;
            
            IsRunning = true;
            _serial.DataProvider.Open(portName);
            _serial.DataProvider.DataReceived += DataReceivedHandler;
        }

        public void StopAndSave(string comment = null)
        {
            if (!IsRunning)
                return;
            
            _serial.DataProvider.DataReceived -= DataReceivedHandler;
            _serial.DataProvider.Close();
            
            _readingsSaver.Save($"./sensors_saved_data/{GetDirectoryName()}", ReadingsMessages, comment);
            
            ReadingsMessages = new List<ReadingsMessage>();
            IsRunning = false;
        }

        private void DataReceivedHandler(object sender, ReadingMessageReceivedEventArgs e)
        {
            foreach (var readingsMessage in e.ReadingsMessages)
            {
                ReadingsMessages.Add(readingsMessage);
            }
        }

        private string GetDirectoryName()
        {
            var now = DateTimeOffset.Now;

            return $"{now.Hour}-{now.Minute}-{now.Second}_{now.Day}-{now.Month}-{now.Year}_{now.ToUnixTimeSeconds()}";
        }
    }
}