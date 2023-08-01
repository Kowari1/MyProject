
using System.Collections.ObjectModel;

namespace MyProject.Models
{
    public class Test
    {
        public Test()
        {
        }
        public Test(string name, ObservableCollection<Question> questions)
        {
            Name = name;
            PathFile = $"D:\\TestProject\\IColectionTest\\test repository\\{name}.tdt";
            Questions = questions;
        }

        public ObservableCollection<Question> Questions { get; set; }
       = new ObservableCollection<Question>();

        private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        private string pathFile;
        public string PathFile
        {
            get => pathFile;
            set => pathFile = value;
        }
    }
}
