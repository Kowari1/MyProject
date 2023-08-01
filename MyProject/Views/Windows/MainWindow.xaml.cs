using MyProject.Services;
using MyProject.ViewModels;
using System.Windows;

namespace MyProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(new BinaryFormatterService());
        }
    }
}
