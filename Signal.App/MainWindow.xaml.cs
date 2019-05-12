using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

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

        private void CommentOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = new TextRange(CommentRichTextBox.Document.ContentStart, CommentRichTextBox.Document.ContentEnd)
                .Text
                .Trim();
            
            ViewModel.CommentText = text;
        }
    }
}