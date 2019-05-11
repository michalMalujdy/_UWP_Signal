using Signal.Core.Services;

namespace Signal.Core.Domain.DataProviding.Serial
{
    public class Serial
    {
        public ISerialDataProvider DataProvider { get; private set; }

        public Serial(ISerialDataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }
    }
}