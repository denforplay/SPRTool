using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Services;
using System;

namespace SPR.Client.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly Func<ViewModelBase> _createViewModel;
        private readonly INavigationService _navigationService;
        private readonly Type _viewModelType;

        public NavigateCommand(INavigationService navigationService, Type viewModelType)
        {
            _navigationService = navigationService;
            _viewModelType = viewModelType;
        }

        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _navigationService.NavigateTo(_viewModelType);
        }
    }
}
