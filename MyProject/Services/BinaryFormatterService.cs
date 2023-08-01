using MyProject.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace MyProject.Services
{
    public class BinaryFormatterService : IFileService
    {
        public void DeleteFile(Test test)
        {
            File.Delete(test.PathFile);
            Tests.tests.Remove(test);
        }

        public Test Open(string filename)
        {
            Test test;
            BinaryFormatter formatter =
                new BinaryFormatter();
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                test = formatter.Deserialize(fs) as Test;
            }

            return test;
        }

        public void Save(Test test)
        {
            BinaryFormatter formatter =
                new BinaryFormatter();
            using (FileStream fs = new FileStream(test.PathFile, FileMode.Create))
            {
                formatter.Serialize(fs, test);
            }
        }
    }
}
