using MyProject.Infrastructure.Commands.Base;
using System;

namespace MyProject.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecut;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null) 
        {
            execute = Execute ?? throw new ArgumentException(nameof(Execute));
            canExecut = CanExecute;
        }

        public override bool CanExecute(object parameter) => canExecut?.Invoke(parameter) ?? true;

        public override void Execute(object parameter) => execute(parameter);
    }
}
