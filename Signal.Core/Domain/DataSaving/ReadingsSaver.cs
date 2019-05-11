using System.Collections.Generic;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Domain.DataProviding.Serial.Message;

namespace Signal.Core.Domain.DataSaving
{
    public class ReadingsSaver
    {
        public string Comment { get; set; }
        public ICollection<ReadingsMessage> ReadingsMessages { get; set; }
    }
}