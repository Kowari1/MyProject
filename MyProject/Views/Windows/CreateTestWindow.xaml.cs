using MyProject.Models;
using MyProject.Services;
using MyProject.ViewModels;
using System.Windows;

namespace MyProject.Views.Windows
{
    public partial class CreateTestWindow : Window 
    {
        public CreateTestWindow(Test test, bool editTest)
        {
            InitializeComponent();
            DataContext = new Create_LoadWindowViewModel(new BinaryFormatterService(), test, editTest);
        }
    }
}
