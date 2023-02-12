using SPR.Client.Abstractions.Core;
using System;

namespace SPR.Client.Commands
{
    public class ActionCommand : CommandBase
    {
        private readonly Action _action;
        private readonly Func<bool>? _canExecute;

        public ActionCommand(Action action, Func<bool>? canExecute = default)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter)
        {
            if(_canExecute is null)
            {
                return true;
            }
            else
            {
                return _canExecute();
            }
        }

        public override void Execute(object? parameter)
        {
            _action();
        }
    }
}
