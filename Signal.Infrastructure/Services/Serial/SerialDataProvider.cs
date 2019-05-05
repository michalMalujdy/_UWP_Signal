using System;
using System.Diagnostics;
using System.IO.Ports;

namespace Signal.Infrastructure.Services.Serial
{
    public class SerialDataProvider
    {
        public event EventHandler DataReceived;

        private readonly SerialPort _serialPort;
        
        public SerialDataProvider(string portName)
        {
            _serialPort = new SerialPort(portName)
            {
                BaudRate = 9600,
                PortName = "COM3",
                Handshake = Handshake.None,
                RtsEnable = true,
                DtrEnable = true
            };

            _serialPort.Open();
            _serialPort.DataReceived += DataReceivedHandler;
        
            Console.WriteLine($"Serial port {portName} is waiting...");
            Console.WriteLine();
            Console.ReadKey();
            _serialPort.Close();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            var message = _serialPort.ReadExisting();
            Console.WriteLine(message);
            //DataReceived?.Invoke(this, e);
        }
    }
}