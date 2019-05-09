using System;
using System.IO.Ports;
using System.Threading.Tasks;
using Autofac;
using Signal.Core.Domain;
using Signal.Core.Domain.DataProviding;
using Signal.Core.Services;
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
            var session = new RecordingSession(serial);
            
            session.Start("COM5");
            while (true)
            {
                Task.Delay(Int32.MaxValue);
            }
            session.StopAndSave("abc.csv");
        }
    }

    private static IContainer SetupDependencyInjectionBuilder()
    {
        var builder = new ContainerBuilder();
        
        builder.RegisterType<SerialDataProvider>().As<ISerialDataProvider>();
        builder.RegisterType<Serial>().AsSelf();

        return builder.Build();
    }
}