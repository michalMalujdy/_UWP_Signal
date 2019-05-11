using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Domain.DataProviding.Serial.Message;
using Signal.Core.Services;

namespace Signal.Infrastructure.Services.FileWriter
{
    public class ReadingsCsvSaver : IReadingsSaver
    {
        public async Task Save(string filename, ICollection<ReadingsMessage> readingsMessages)
        {
            CreateDirectoriesIfDontExist(filename);
            SetDotFloatingPointSeparator();
            await WriteReadingsToFile(filename, readingsMessages);
        }

        private async Task WriteReadingsToFile(string filename, ICollection<ReadingsMessage> readingsMessages)
        {
            using (var writer = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write)))
            {
                await WriteHeaderAsync(writer);

                foreach (var readingsMessage in readingsMessages)
                {
                    foreach (var reading in readingsMessage.Readings)
                    {
                        var line = $"{reading.DeviceId},{reading.Value}\n";
                        await writer.WriteAsync(line);
                    }
                }

                await writer.FlushAsync();
            }
        }

        private void SetDotFloatingPointSeparator()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }

        private void CreateDirectoriesIfDontExist(string filename)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
        }

        private async Task WriteHeaderAsync(StreamWriter writer)
        {
            await writer.WriteAsync("DeviceId,Value\n");
        }
    }
}