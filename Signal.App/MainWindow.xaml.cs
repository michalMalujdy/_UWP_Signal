using System.Windows;
using Autofac;
using Signal.Core.Domain;
using Signal.Core.Domain.DataProviding.Serial;
using Signal.Core.Domain.DataProviding.Serial.SerialTransmission;
using Signal.Core.Services;
using Signal.Infrastructure.Services.FileWriter;
using Signal.Infrastructure.Services.Serial;

namespace Signal.App
{
    public partial class MainWindow
    {
        public MainWindowViewModel ViewModel { get; set; } = new MainWindowViewModel();
        
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.OnButtonClick(this, e);
        }
    }
}