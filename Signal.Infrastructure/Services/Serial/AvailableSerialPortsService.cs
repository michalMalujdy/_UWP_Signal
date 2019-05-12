using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Timers;
using Signal.Core.Services.IAvailableSerialPorts;

namespace Signal.Infrastructure.Services.Serial
{
    public class AvailableSerialPortsService : IAvailableSerialPortsService
    {
        private ICollection<string> _previouslyAvailablePorts = SerialPort.GetPortNames();
        public event EventHandler<AvailablePortsChangedEventArgs> AvailablePortsChanged;

        public AvailableSerialPortsService()
        {
            var timer = new Timer(1000);
            timer.Elapsed += CheckConnectedPortsState;
            timer.Enabled = true;
        }

        private void CheckConnectedPortsState(object sender, ElapsedEventArgs e)
        {
            var currentlyAvailablePorts = GetAvailablePorts();

            if (IsPortsCollectionChanged(_previouslyAvailablePorts, currentlyAvailablePorts))
            {
                AvailablePortsChanged?.Invoke(this, 
                    new AvailablePortsChangedEventArgs() {AvailablePorts = currentlyAvailablePorts});

                _previouslyAvailablePorts = currentlyAvailablePorts;
            }
        }

        public ICollection<string> GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        private bool IsPortsCollectionChanged(ICollection<string> previous, ICollection<string> current)
        {
            if (previous.Count != current.Count)
                return true;

            var portsRemovedCount = previous.Count(p => !current.Contains(p));
            if (portsRemovedCount > 0)
                return true;

            var portsAddedCount = current.Count(c => !previous.Contains(c));
            if (portsAddedCount > 0)
                return true;

            return false;
        }
    }
}