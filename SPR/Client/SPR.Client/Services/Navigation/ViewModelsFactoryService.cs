using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Services;
using System;
using System.Collections.Generic;

namespace SPR.Client.Services.Navigation
{
    public class ViewModelsFactoryService : IViewModelsFactoryService
    {
        private readonly Dictionary<Type, Func<ViewModelBase>> _viewModels;

        public ViewModelsFactoryService(Dictionary<Type, Func<ViewModelBase>> viewModels)
        {
            _viewModels = viewModels;
        }

        public ViewModelBase? CreateViewModel(Type viewModelType)
        {
            return _viewModels.TryGetValue(viewModelType, out var viewModel) ? viewModel() : null;
        }

        public ViewModelBase? CreateViewModel<T>() where T : ViewModelBase
        {
            return CreateViewModel(typeof(T));
        }
    }
}
