using System.Collections.Generic;
using Signal.Core.Domain.DataProviding;

namespace Signal.Core.Domain.DataSaving
{
    public class CsvSaver
    {
        public string Comment { get; set; }
        public ICollection<ReadingsMessage> ReadingsMessages { get; set; }
    }
}