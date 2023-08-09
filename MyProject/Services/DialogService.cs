using System;
using System.Collections.Generic;
using System.Windows;

namespace MyProject.Services
{
    public interface IDialogService
    {
        void ShowDialog(string name, Action<string> callback);

        void ShowDialog<ViewModel>(Action<string> callback);
    }

    class DialogService : IDialogService
    {
        static Dictionary<Type, Type> mapings = new Dictionary<Type, Type>();

        public static void RegisterDialog<TView, TViewModel>()
        {
            mapings.Add(typeof(TView), typeof(TViewModel));
        }

        public void ShowDialog(string name, Action<string> callback)
        {
            var type = Type.GetType($"DialogTest.{name}");
            ShowDialogInternal(type, callback, null);
        }

        public void ShowDialog<TViewModel>(Action<string> callback)
        {
            var type = mapings[typeof(TViewModel)];
            ShowDialogInternal(type, callback, typeof(TViewModel));
        }

        public void ShowDialogInternal(Type type, Action<string> callback, Type vmType)
        {

            var dialog = new DialogWindow();

            EventHandler closeEventHandler = null;
            closeEventHandler = (s, e) =>
            {
                callback(dialog.DialogResult.ToString());
                dialog.Closed -= closeEventHandler;
            };
            dialog.Closed += closeEventHandler;

            var content = Activator.CreateInstance(type);

            if (vmType != null)
            {
                var vm = Activator.CreateInstance(vmType);
                (content as FrameworkElement).DataContext = vm;
            }

            dialog.Content = Activator.CreateInstance(type);

            dialog.ShowDialog();
        }
    }
}
