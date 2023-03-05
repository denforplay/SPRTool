using System.Windows.Input;

namespace SPR.Client.Abstractions.Core
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public virtual void Execute(object? parameter)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public void RaiseExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
