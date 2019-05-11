using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Autofac;
using Signal.App.Annotations;
using Signal.Core.Domain;
using Signal.Core.Domain.DataProviding.Serial;
using Signal.Core.Domain.DataProviding.Serial.SerialTransmission;
using Signal.Core.Services;
using Signal.Infrastructure.Services.FileWriter;
using Signal.Infrastructure.Services.Serial;
using IContainer = Autofac.IContainer;

namespace Signal.App
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private const string StartText = "Start recording";
        private const string StopText = "Stop and save";

        public string ButtonText 
        {
            get { return _session.IsRunning ? StopText : StartText; }
        }
        
        private readonly RecordingSession _session;

        public MainWindowViewModel()
        {
            var builder = SetupDependencyInjectionBuilder();

            using (var scope = builder.BeginLifetimeScope())
            {
                var serial = scope.Resolve<Serial>();
                var readingsSaver = scope.Resolve<IReadingsSaver>();
                _session = new RecordingSession(serial, readingsSaver);
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

        public void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (!_session.IsRunning)
                _session.Start("COM5");
            
            else
                _session.StopAndSave();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonText)));
        }
    }
}