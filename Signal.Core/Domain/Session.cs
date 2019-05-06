using System.Collections.Generic;
using Signal.Core.Domain.DataProviding;

namespace Signal.Core.Domain
{
    public class Session
    {
        public bool IsRunning { get; private set; }
        public ICollection<ReadingsMessage> ReadingsMessages { get; private set; } 
            = new List<ReadingsMessage>();

        public void Start()
        {
            // instantiate new Serial
            // hook up the message received method
            IsRunning = true;
        }

        public void StopAndSave(string filename)
        {
            // Write ReadingsMessages to csv file
            ReadingsMessages = new List<ReadingsMessage>();
            IsRunning = false;
        }
    }
}