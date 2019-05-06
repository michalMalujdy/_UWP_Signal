using System.Collections.Generic;

namespace Signal.Core.Domain.DataProviding
{
    public class ReadingsMessage
    {
        public ICollection<Reading> Readings { get; set; }
    }
}