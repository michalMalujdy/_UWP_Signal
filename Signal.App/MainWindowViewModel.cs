using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
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
        
        public string ButtonText => _session.IsRunning ? StopText : StartText;
        private const string StartText = "Start recording";
        private const string StopText = "Stop and save";

        public Brush StatusLedBrush => _session.IsRunning ? Brushes.Lime : Brushes.Red;
        public string StatusLedText => _session.IsRunning ? RunningText : OnHoldText;
        private const string RunningText = "Recording...";
        private const string OnHoldText = "Not recording";

        public string CommentText { get; set; }

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
                _session.StopAndSave(GetComment());

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonText)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusLedBrush)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusLedText)));
        }

        private string GetComment()
        {
            return string.IsNullOrEmpty(CommentText) ? null : CommentText;
        }
    }
}