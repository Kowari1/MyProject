using MyProject.ViewModels.Base;
using System.Collections.ObjectModel;
using MyProject.Models;
using System.Linq;
using MyProject.Infrastructure.Commands;
using MyProject.Services;
using System.Windows;

namespace MyProject.ViewModels
{
    public class Create_LoadWindowViewModel : ViewModel
    {
        bool editTest;
        Test test;

        IFileService fileService;
        IDialogService dialogService = new DialogService();

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
            }
        }

        #region COMMANND
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

        private LambdaCommand removeQuestion;
        public LambdaCommand RemoveQuestion
        {
            get
            {
                return removeQuestion ??
                  (removeQuestion = new LambdaCommand(obj =>
                  {
                      Questions.Remove(SelectedQuestion);
                  }));
            }
        }

        private LambdaCommand addItem;
        public LambdaCommand AddItem
        {
            get
            {
                return addItem ??
                  (addItem = new LambdaCommand(obj =>
                  {
                      if (SelectedQuestion.Items[0].GetType() == new RadioButtonItem().GetType())
                          SelectedQuestion.Items.Add(new RadioButtonItem());
                      else if (SelectedQuestion.Items[0].GetType() == new CheckBoxItem().GetType())
                          SelectedQuestion.Items.Add(new CheckBoxItem());
                  }));
            }
        }

        private LambdaCommand removeItem;
        public LambdaCommand RemoveItem
        {
            get
            {
                return removeItem ??
                  (removeItem = new LambdaCommand(obj =>
                  {

                      if (SelectedQuestion.Items.Count > 1)
                          SelectedQuestion.Items.Remove(selectedItem);
                  }));
            }
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
                      if (Tests.tests.Count != 0)
                          foreach (Test test in Tests.tests)
                          {
                              if (test.Name == TestName && test != this.test || TestName == null)
                              {
                                  MessageBox.Show("тест с таким именем уже существует");
                                  
                                  break;
                              }
                              else
                              {
                                  if (editTest == true)
                                  {
                                      fileService.DeleteFile(test);
                                      AddTest();
                                  }
                                  else
                                  {
                                      AddTest();
                                  }
                                  break;
                              }
                          }
                      else
                          AddTest();
                  }));
            }
        }
        #endregion

        #region
        private void AddAnyQuestion<T>(T item)
            where T : IItem, new()
        {
            var questionItem = new Question() { QuestionName = "" + Questions.Count };
            questionItem.Items.Add(item);
            Questions.Add(questionItem);
            SelectedItem = item;
        }

        private void AddTest()
        {
            Test newTest = new Test(TestName, Questions);
            fileService.Save(newTest);
            Tests.tests.Add(newTest);
        }
        #endregion
        public Create_LoadWindowViewModel(IFileService fileService, Test test, bool editTest)
        {
            this.fileService = fileService;
            Questions = test.Questions;
            TestName = test.Name;
            this.test = test;
            this.editTest = editTest;
        }
    }
}
