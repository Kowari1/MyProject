using MyProject.Infrastructure.Commands;
using MyProject.Models;
using MyProject.Services;
using MyProject.ViewModels.Base;
using System.Collections.ObjectModel;
using System.IO;


namespace MyProject.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        IFileService fileService;
        IWindowService windowService = new WindowSerwice();

        public ObservableCollection<Test> TestsCollection { get; set; }

        private Test selectedTest;
        public Test SelectedTest
        {
            get => selectedTest;
            set
            {
                Set(ref selectedTest, value);
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
                      windowService.ShowWindow("CreateTestWindow", new Create_LoadWindowViewModel(new BinaryFormatterService(),new Test(), false));
                  }));
            }
        }

        private LambdaCommand remove;
        public LambdaCommand Remove
        {
            get
            {
                return remove ??
                  (remove = new LambdaCommand(obj =>
                  {
                      fileService.DeleteFile(SelectedTest);
                  }));
            }
        }

        private LambdaCommand edit;
        public LambdaCommand Edit
        {
            get
            {
                return edit ??
                  (edit = new LambdaCommand(obj =>
                  {
                      windowService.ShowWindow("CreateTestWindow", new Create_LoadWindowViewModel(new BinaryFormatterService(), SelectedTest, true));
                  }));
            }
        }

        private LambdaCommand play;
        public LambdaCommand Play
        {
            get
            {
                return play ??
                  (play = new LambdaCommand(obj =>
                  {
                      windowService.ShowWindow("LoadTestWindow", new LoadTestViewModel(SelectedTest));
                  }));
            }
        }

        public MainWindowViewModel(IFileService fileService)
        {
            this.fileService = fileService;
            var filesPath = Directory.GetFiles("D:\\MainProject\\MyProject\\test repository\\", "*.tdt");
            foreach (var file in filesPath)
            {
                Tests.tests.Add(fileService.Open(file));
            }

            TestsCollection = Tests.tests;
        }
    }
}
