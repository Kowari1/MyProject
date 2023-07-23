using MyProject.ViewModels.Base;
using System.Collections.ObjectModel;
using MyProject.Models;
using System.Linq;
using MyProject.Infrastructure.Commands;
using MyProject.Models;
using System.Windows.Input;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MyProject.ViewModels
{
    internal class CreateTestWindowViewModel : ViewModel
    {
        public ObservableCollection<Question> Questions { get; set; }
            = new ObservableCollection<Question>();

        private Question selectedQuestion;
        public Question SelectedQuestion
        {
            get => selectedQuestion;
            set
            {
                Set(ref selectedQuestion, value);
                if (selectedItem != null && value != null)
                    SelectedItem = value.Items[0];
            }
        }

        private IItem selectedItem;
        public IItem SelectedItem
        {
            get => selectedItem;
            set
            {
                Set(ref selectedItem, value);
            }
        }

        public string testName;
        public string TestName
        {
            get => testName;
            set
            {
                Set(ref testName, value);
                Tests.tests.Last().Name = value;
            }
        }

        private LambdaCommand createQuestionCheckBoxItems;
        public LambdaCommand CreateQuestionCheckBoxItems
        {
            get
            {
                return createQuestionCheckBoxItems ??
                  (createQuestionCheckBoxItems = new LambdaCommand(obj =>
                  {
                      AddAnyQuestion(new CheckBoxItem());
                  }));
            }
        }

        private LambdaCommand createQuestionRadioButtomItems;
        public LambdaCommand CreateQuestionRadioButtomItems
        {
            get
            {
                return createQuestionRadioButtomItems ??
                  (createQuestionRadioButtomItems = new LambdaCommand(obj =>
                  {
                      AddAnyQuestion(new RadioButtonItem());
                  }));
            }
        }

        private LambdaCommand createQuestionTextItem;
        public LambdaCommand CreateQuestionTextItem
        {
            get
            {
                return createQuestionTextItem ??
                  (createQuestionTextItem = new LambdaCommand(obj =>
                  {
                      AddAnyQuestion(new TextItem());
                  }));
            }
        }

        public ICommand RemoveQuestion { get; }
        private bool CanRemoveQuestionExecute(object p) => p is Question qestion && Questions.Contains(qestion);
        private void OnanotherRemoveQuestionExecute(object p)
        {
            if (!(p is Question qestion)) return;
            var qestion_index = Questions.IndexOf(qestion);
            Questions.Remove(qestion);
            if (qestion_index < Questions.Count)
                SelectedQuestion = Questions[qestion_index];
        }

        public ICommand AddItem { get; }
        private bool CanAddItemExecute(object p) => p is Question qestion && Questions.Contains(qestion);
        private void OnAddItemItemExecute(object p)
        {
            if (SelectedQuestion.Items[0].GetType() == new RadioButtonItem().GetType())
                SelectedQuestion.Items.Add(new RadioButtonItem());
            else if (SelectedQuestion.Items[0].GetType() == new CheckBoxItem().GetType())
                SelectedQuestion.Items.Add(new CheckBoxItem());
        }

        public ICommand RemoveItem { get; }
        private bool CanRemoveItemExecute(object p) => p is Question qestion && Questions.Contains(qestion);
        private void OnanotherRemoveItemExecute(object p)
        {
            if (SelectedQuestion.Items.Count > 1)
                SelectedQuestion.Items.Remove(selectedItem);
        }

        private LambdaCommand radioButtonChekced;
        public LambdaCommand RadioButtonChekced
        {
            get
            {
                return radioButtonChekced ??
                  (radioButtonChekced = new LambdaCommand(obj =>
                  {
                      RadioButtonItem selectedChekcedItem = (RadioButtonItem)selectedItem;
                      selectedChekcedItem.CorrectAnswer = true;
                      foreach (RadioButtonItem item in selectedQuestion.Items.Cast<RadioButtonItem>())
                      {
                          if (item != selectedChekcedItem)
                              item.CorrectAnswer = false;
                      }
                  }));
            }
        }

        private LambdaCommand createTest;
        public LambdaCommand CreateTest
        {
            get
            {
                return createTest ??
                  (createTest = new LambdaCommand(obj =>
                  {
                      BinaryFormatter formatter = new BinaryFormatter();
                      using (FileStream transporFileStream = new FileStream("test.tdt", FileMode.OpenOrCreate))
                      {
                          formatter.Serialize(transporFileStream, Questions);
                      }
                  }));
            }
        }

        private void AddAnyQuestion<T>(T item)
            where T : IItem, new()
        {
            var questionItem = new Question() { QuestionName = "" + Questions.Count };
            questionItem.Items.Add(new T());
            Questions.Add(questionItem);
        }

        public CreateTestWindowViewModel()
        {
            RemoveQuestion = new LambdaCommand(OnanotherRemoveQuestionExecute, CanRemoveQuestionExecute);
            AddItem = new LambdaCommand(OnAddItemItemExecute, CanAddItemExecute);
            RemoveItem = new LambdaCommand(OnanotherRemoveItemExecute, CanRemoveItemExecute);
        }
    }
}
