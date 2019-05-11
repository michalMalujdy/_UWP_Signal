using System.Collections.Generic;
using System.Threading.Tasks;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Domain.DataProviding.Serial.Message;

namespace Signal.Core.Services
{
    public interface IReadingsSaver
    {
        Task Save(string filename, ICollection<ReadingsMessage> readingsMessages);
    }
}