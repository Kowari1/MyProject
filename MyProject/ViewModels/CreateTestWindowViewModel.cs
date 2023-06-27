using MyProject.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MyProject.ViewModels
{
    internal class CreateTestWindowViewModel : ViewModel
    {
        public ObservableCollection<object> inerphaseItem = new ObservableCollection<object>()
        {
            new TextBlock(),
            new TextBlock()
        };

        public CreateTestWindowViewModel()
        {

        }
    }
}
