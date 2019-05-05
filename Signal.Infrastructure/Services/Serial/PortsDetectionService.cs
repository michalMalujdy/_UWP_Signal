using System;
using System.IO.Ports;

namespace Signal.Infrastructure.Services.Serial
{
    public class PortsDetectionService
    {
        public PortsDetectionService()
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