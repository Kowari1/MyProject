using System;
using System.Collections.ObjectModel;

namespace MyProject.Models
{
    [Serializable]
    public class Question
    {
        private string questionName;
        public string QuestionName
        {
            get => questionName;
            set => questionName = value;
        }

        public ObservableCollection<IItem> Items { get; set; }
        = new ObservableCollection<IItem>();
    }
}
