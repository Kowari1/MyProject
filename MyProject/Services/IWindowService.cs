using System;
using System.Windows;

namespace MyProject.Services
{
    public interface IWindowService
    {
        void ShowWindow<T>(string name, T vm)
            where T : class;
    }

    public class WindowSerwice : IWindowService
    {
        public void ShowWindow<T>(string name, T vm)
            where T : class
        {
            var type = Type.GetType($"MyProject.Views.Windows.{name}");
            ShowWindowInternal(type, vm);
        }

        public void ShowWindowInternal<T>(Type type, T vm)
            where T : class
        {
            Window window = (Window)Activator.CreateInstance(type);
            window.DataContext = vm;
            window.Show();
        }
    }
}
