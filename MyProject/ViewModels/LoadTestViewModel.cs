using MyProject.Infrastructure.Commands;
using MyProject.Models;
using MyProject.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace MyProject.ViewModels
{
    public class LoadTestViewModel : ViewModel
    {
        public Test test { get; }

        public ObservableCollection<Question> LoadTest { get; }
        = new ObservableCollection<Question>();

        public ObservableCollection<Question> ReferenseTest { get; set; }
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
                      foreach (RadioButtonItem item in selectedQuestion.Items)
                      {
                          if (item != selectedChekcedItem)
                              item.CorrectAnswer = false;
                      }
                  }));
            }
        }

        private LambdaCommand finish;
        public LambdaCommand Finis
        {
            get
            {
                return finish ??
                  (finish = new LambdaCommand(obj =>
                  {
                      int count = 0;
                      for (int i = 0; i < LoadTest.Count; i++)
                      {
                          for (int j = 0; j < LoadTest[i].Items.Count; j++)
                          {
                              if (LoadTest[i].Items[j].Equals(ReferenseTest[i].Items[j]) == false)
                              {
                                  count++;
                                  break;
                              }
                          }
                      }
                      MessageBox.Show("Неправельных ответов " + count);
                  }));
            }
        }

        #region METHODS
        public static T DeepClone<T>(T obj)
        {
            var formatter = new BinaryFormatter();

            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion

        public LoadTestViewModel(Test test)
        {
            this.test = test;
            LoadTest = test.Questions;
            ReferenseTest = DeepClone(test.Questions);

            foreach (var Question in ReferenseTest)
            {
                foreach (var item in Question.Items)
                {
                    if (item.GetType() == new RadioButtonItem().GetType())
                    {
                        RadioButtonItem radioButtomItem = (RadioButtonItem)item;
                        radioButtomItem.CorrectAnswer = false;
                    }
                    else if (item.GetType() == new CheckBoxItem().GetType())
                    {
                        CheckBoxItem checkBoxItem = (CheckBoxItem)item;
                        checkBoxItem.CorrectAnswer = false;
                    }
                    else
                    {
                        TextItem textItem = (TextItem)item;
                        textItem.CorrectAnswer = null;
                    }
                }
            }
        }
    }
}
