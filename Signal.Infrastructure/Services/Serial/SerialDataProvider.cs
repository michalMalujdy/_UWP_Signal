using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using Newtonsoft.Json;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Domain.DataProviding.Serial.Message;
using Signal.Core.Domain.DataProviding.Serial.SerialTransmission;
using Signal.Core.Services;

namespace Signal.Infrastructure.Services.Serial
{
    public class SerialDataProvider : ISerialDataProvider
    {
        public event EventHandler<ReadingMessageReceivedEventArgs> DataReceived;

        private SerialPort _serialPort;
        private readonly SerialTransmission _serialTransmission;

        public SerialDataProvider(SerialTransmission serialTransmission)
        {
            _serialTransmission = serialTransmission;
            _serialTransmission.FullMessagesReceived += OnFullMessagesReceived;
        }

        public void Open(string portName)
        {
            _serialPort = new SerialPort()
            {
                BaudRate = 9600,
                PortName = portName,
                Handshake = Handshake.None,
                RtsEnable = true,
                DtrEnable = true
            };

            _serialPort.Open();
            _serialPort.DataReceived += DataReceivedHandler;
        
            Console.WriteLine($"Serial port {portName} is waiting...");
        }

        public void Close()
        {
            _serialPort.Close();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var message = _serialPort.ReadExisting();
            Console.WriteLine(message);
            
            _serialTransmission.ReadNext(message);
        }

        private void OnFullMessagesReceived(object sender, FullMessageReceivedEventArgs e)
        {
            var readingsEvent = new ReadingMessageReceivedEventArgs()
            {
                ReadingsMessages = e.ReadingsMessages
            };
            
            DataReceived?.Invoke(this, readingsEvent);
        }
    }
}