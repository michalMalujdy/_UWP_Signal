using System;
using System.IO.Ports;
using Signal.Core.Services.IAvailableSerialPorts;

namespace Signal.Infrastructure.Services.Serial
{
    public class AvailableSerialPortsService : IAvailableSerialPortsService
    {
        public AvailableSerialPortsService()
        {
            var ports = SerialPort.GetPortNames();
            
            if (ports.Length < 1)
                return;

            foreach (var port in ports)
            {
                Console.WriteLine(port);
            }
        }
    }
}