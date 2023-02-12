using SPR.Client.Abstractions.Core;

namespace SPR.Client.Abstractions.Services
{
    public interface IViewModelsFactoryService
    {
        ViewModelBase? CreateViewModel(Type viewModelType);
        ViewModelBase? CreateViewModel<T>() where T : ViewModelBase;
    }
}
