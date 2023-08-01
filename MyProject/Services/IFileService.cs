using MyProject.Models;

namespace MyProject.Services
{
    public interface IFileService
    {
        Test Open(string filename);
        void Save(Test test);
        void DeleteFile(Test test);
    }
}
