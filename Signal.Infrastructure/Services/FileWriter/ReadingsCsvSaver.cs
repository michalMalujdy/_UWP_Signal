using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Signal.Core.Domain.DataProviding.Serial.Message;
using Signal.Core.Services;

namespace Signal.Infrastructure.Services.FileWriter
{
    public class ReadingsCsvSaver : IReadingsSaver
    {
        public async Task Save(string directory, ICollection<ReadingsMessage> readingsMessages, string comment = null)
        {
            SetDotFloatingPointSeparator();
            
           await WriteReadingsToFile(directory, readingsMessages);

            if (comment != null)
                await WriteCommentToFile(directory, comment);
        }

        private async Task WriteReadingsToFile(string directory, ICollection<ReadingsMessage> readingsMessages)
        {
            var readingsFilepath = $"{directory}\\sensors_data.csv";
            CreateDirectoriesIfDontExist(readingsFilepath);
            
            using (var writer = new StreamWriter(new FileStream(readingsFilepath, FileMode.Create, FileAccess.Write)))
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
            }
        }

        private async Task WriteCommentToFile(string directory, string comment)
        {
            var commentFilepath = $"{directory}\\comment.txt";
            CreateDirectoriesIfDontExist(commentFilepath);

            using (var writer = new StreamWriter(new FileStream(commentFilepath, FileMode.Create, FileAccess.Write)))
            {
                await writer.WriteAsync(comment);
            }
        }

        private void SetDotFloatingPointSeparator()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }

        private void CreateDirectoriesIfDontExist(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        private async Task WriteHeaderAsync(StreamWriter writer)
        {
            await writer.WriteAsync("DeviceId,Value\n");
        }
    }
}