using System;
using System.Diagnostics;
using System.IO.Ports;
using Newtonsoft.Json;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Services;

namespace Signal.Infrastructure.Services.Serial
{
    public class SerialDataProvider : ISerialDataProvider
    {
        public event EventHandler<ReadingMessageReceivedEventArgs> DataReceived;

        private SerialPort _serialPort;

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
            
            var readingsMessage = JsonConvert.DeserializeObject<ReadingsMessage>(message);
            var readingsEvent = new ReadingMessageReceivedEventArgs() {ReadingsMessage = readingsMessage};
            DataReceived?.Invoke(this, readingsEvent);
        }
    }
}