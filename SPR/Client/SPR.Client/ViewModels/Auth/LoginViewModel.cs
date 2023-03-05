using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.API.Requests;
using System.Windows.Input;

namespace SPR.Client.ViewModels.Auth
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        public ICommand LoginCommand { get; }

        private readonly IAuthHttpService _authHttpService;

        public LoginViewModel(IAuthHttpService authHttpService)
        {
            LoginCommand = new ActionCommand(LoginAsync);
            _authHttpService = authHttpService;
        }

        public string UserName
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(UserName);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(Password);
            }
        }

        private async void LoginAsync()
        {
            var loginRequest = new LoginRequest
            {
                Username = UserName,
                Password = Password
            };

            var response = await _authHttpService.LoginAsync(loginRequest);
        }
    }
}
