using MyProject.Infrastructure.Commands;
using MyProject.ViewModels.Base;
using MyProject.Views.Windows;
using System.Windows.Input;

namespace MyProject.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Header MainWindow
        private string title = "Create Test";

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }
        #endregion

        #region Create Commands

        #region Buttons Commands
        public ICommand ShowTestWindow { get; }

        private void OnShowTestWindowExecuted(object parameters)
        {
            LoadTestWindow window = new LoadTestWindow();
            window.Show();
        }

        private bool CanShowTestWindowExecuted(object parameter) => true;

        public ICommand ShowCreateTestWindow { get; }

        private void OnShowCreateTestsWindowExecuted(object parameters)
        {
            CreateTestWindow window = new CreateTestWindow();
            window.Show();
        }

        private bool CanShowCreateTestsWindowExecuted(object parameter) => true;
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Initialization Commands
            ShowTestWindow = new LambdaCommand(OnShowTestWindowExecuted, CanShowTestWindowExecuted);

            ShowCreateTestWindow = new LambdaCommand(OnShowCreateTestsWindowExecuted, CanShowCreateTestsWindowExecuted);
            #endregion
        }
    }
}
