using System;
using System.IO.Ports;
using System.Threading.Tasks;
using Autofac;
using Signal.Core.Domain;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Domain.DataProviding.Serial;
using Signal.Core.Domain.DataProviding.Serial.SerialTransmission;
using Signal.Core.Services;
using Signal.Infrastructure.Services.FileWriter;
using Signal.Infrastructure.Services.Serial;

class Program 
{ 
    [STAThread] 
    static void Main(string[] args)
    {
        var builder = SetupDependencyInjectionBuilder();

        using (var scope = builder.BeginLifetimeScope())
        {
            var serial = scope.Resolve<Serial>();
            var readingsSaver = scope.Resolve<IReadingsSaver>();
            var session = new RecordingSession(serial, readingsSaver);
            
            session.Start("COM5");

            var task = Task.Run(async () => { await Task.Delay(6000); });
            task.Wait();
            
            session.StopAndSave();
        }
    }

    private static IContainer SetupDependencyInjectionBuilder()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterType<SerialDataProvider>().As<ISerialDataProvider>();
        builder.RegisterType<Serial>().AsSelf();
        builder.RegisterType<ReadingsCsvSaver>().As<IReadingsSaver>();
        builder.RegisterType<SerialTransmission>().AsSelf();

        return builder.Build();
    }
}