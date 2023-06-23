using MyProject.Infrastructure.Commands.Base;
using System;

namespace MyProject.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> action;
        private readonly Func<object, bool> canExecut;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null) 
        {
        
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (parameter == null) { }
        }
    }
}
