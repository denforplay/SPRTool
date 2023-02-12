using SPR.Client.Abstractions.Core;

namespace SPR.Client.Abstractions.Services
{
    public interface INavigationService
    {
        ViewModelBase CurrentViewModel { get; }
        event Action<ViewModelBase> OnViewModelChanged;
        void NavigateTo(Type applicationViewModelType);
        void NavigateTo<T>() where T : ViewModelBase;
        void NavigateTo(ViewModelBase applicationBaseViewModel);
    }
}
