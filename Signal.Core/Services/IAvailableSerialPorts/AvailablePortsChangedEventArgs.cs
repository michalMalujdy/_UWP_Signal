using System.Collections.Generic;

namespace Signal.Core.Services.IAvailableSerialPorts
{
    public class AvailablePortsChangedEventArgs
    {
        public ICollection<string> AvailablePorts { get; set; }
    }
}