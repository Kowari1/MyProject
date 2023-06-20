using MyProject.ViewModels.Base;

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
    }
}
