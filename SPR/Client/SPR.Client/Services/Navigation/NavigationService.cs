using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Services;
using System;

namespace SPR.Client.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IViewModelsFactoryService _viewModelsFactoryService;

        public NavigationService(IViewModelsFactoryService viewModelFactoryService)
        {
            _viewModelsFactoryService = viewModelFactoryService;
        }


        public ViewModelBase CurrentViewModel { get; protected set; }

        public event Action<ViewModelBase> OnViewModelChanged;

        public void NavigateTo(Type applicationViewModelType)
        {
            TryCreateNewViewModel(applicationViewModelType);
        }

        public void NavigateTo<T>() where T : ViewModelBase
        {
            TryCreateNewViewModel(typeof(T));
        }

        public void NavigateTo(ViewModelBase applicationBaseViewModel)
        {
            SetNewViewModel(applicationBaseViewModel);
        }

        private void TryCreateNewViewModel(Type viewModelType)
        {
            var viewModel = _viewModelsFactoryService.CreateViewModel(viewModelType);
            if (viewModel is null)
            {
                throw new ArgumentNullException($"Cannot create {viewModel}. Probably you haven't " +
                                              $"registered view models for they creating");
            }
            SetNewViewModel(viewModel);
        }

        private void SetNewViewModel(ViewModelBase viewModel)
        {
            CurrentViewModel = viewModel;
            OnViewModelChanged?.Invoke(viewModel);
        }
    }
}
