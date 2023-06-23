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
        public ICommand ShowTestsWindow { get; }

        private void OnShowTestsWindowExecuted(object parameters)
        {
            LoadTestsWindow window = new LoadTestsWindow();
            window.Show();
        }

        private bool CanShowTestsWindowExecuted(object parameter) => true;

        public ICommand ShowCreateTestsWindow { get; }

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
            ShowTestsWindow = new LambdaCommand(OnShowTestsWindowExecuted, CanShowTestsWindowExecuted);

            ShowCreateTestsWindow = new LambdaCommand(OnShowCreateTestsWindowExecuted, CanShowCreateTestsWindowExecuted);
            #endregion
        }
    }
}
