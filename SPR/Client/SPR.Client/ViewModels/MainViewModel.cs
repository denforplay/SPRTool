using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Services;
using SPR.Client.Commands;
using System.Windows.Input;

namespace SPR.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ICommand HomeNavigationCommand { get; }
        public ICommand CourseManagementNavigationCommand { get; }
        public ICommand StudentManagementNavigationCommand { get; }

        public ViewModelBase CurrentViewModel => _navigationService.CurrentViewModel;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            HomeNavigationCommand = new NavigateCommand(_navigationService, typeof(HomeViewModel));
            StudentManagementNavigationCommand = new NavigateCommand(_navigationService, typeof(StudentManagementViewModel));
            _navigationService.OnViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged(ViewModelBase viewModelBase)
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
