using System;
using System.Collections.Generic;

namespace Signal.Core.Services.IAvailableSerialPorts
{
    public interface IAvailableSerialPortsService
    {
        event EventHandler<AvailablePortsChangedEventArgs> AvailablePortsChanged;

        ICollection<string> GetAvailablePorts();
    }
}