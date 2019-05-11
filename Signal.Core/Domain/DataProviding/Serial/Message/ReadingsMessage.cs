using System.Collections.Generic;

namespace Signal.Core.Domain.DataProviding.Serial.Message
{
    public class ReadingsMessage
    {
        public ICollection<Reading> Readings { get; set; }
    }
}