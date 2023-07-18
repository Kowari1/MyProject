using MyProject.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MyProject.ViewModels
{
    internal class CreateTestWindowViewModel : ViewModel
    {
        public ObservableCollection<string> inerphaseItemems = new ObservableCollection<string>()
        {
            "Test"
        };

        public CreateTestWindowViewModel()
        {

        }
    }
}
