using System.Windows.Input;

namespace SPR.Client.Abstractions.Core.Interfaces
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
    }
}
