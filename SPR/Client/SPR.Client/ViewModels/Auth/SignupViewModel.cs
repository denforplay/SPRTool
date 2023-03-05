using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.API.Requests;
using System.Windows.Input;

namespace SPR.Client.ViewModels.Auth
{
    public class SignupViewModel : ViewModelBase
    {
        private string _name;
        private string _surname;
        private string _username;
        private string _password;
        public ICommand SignupCommand { get; }

        private readonly IAuthHttpService _authHttpService;

        public SignupViewModel(IAuthHttpService authHttpService)
        {
            SignupCommand = new ActionCommand(SignupAsync);
            _authHttpService = authHttpService;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(Name);
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(Surname);
            }
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

        private async void SignupAsync()
        {
            var signupRequest = new SignupRequest
            {
                Name = Name,
                Surname = Surname,
                Username = UserName,
                Password = Password
            };

            var response = await _authHttpService.SignupAsync(signupRequest);
        }
    }
}
