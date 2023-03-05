using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using System.Windows.Input;

namespace SPR.Client.ViewModels.Auth
{
    public class AuthViewModel : ViewModelBase
    {
        private readonly IAuthHttpService _authHttpService;
        private ViewModelBase _currentViewModel;
        public ICommand ShowLoginViewModelCommand { get; }
        public ICommand ShowSignupViewModelCommand { get; }

        public AuthViewModel(IAuthHttpService authHttpService)
        {
            _authHttpService = authHttpService;
            ShowLoginViewModelCommand = new ActionCommand(ShowLoginViewModel);
            ShowSignupViewModelCommand = new ActionCommand(ShowSignupViewModel);
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private void ShowLoginViewModel()
        {
            CurrentViewModel = new LoginViewModel(_authHttpService);
        }

        private void ShowSignupViewModel()
        {
            CurrentViewModel = new SignupViewModel(_authHttpService);
        }
    }
}