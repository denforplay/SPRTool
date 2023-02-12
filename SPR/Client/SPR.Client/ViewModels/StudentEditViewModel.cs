using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Group;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SPR.Client.ViewModels
{
    public class StudentEditViewModel : ViewModelBase
    {
        private readonly IStudentHttpService _studentHttpService;
        private readonly IGroupHttpService _groupHttpService;
        private string _studentNameInput;
        private string _studentSurnameInput;
        private GroupModel _selectedGroup;
        private ObservableCollection<GroupModel> _groups;

        public StudentEditViewModel(IStudentHttpService studentHttpService, IGroupHttpService groupHttpService)
        {
            _studentHttpService = studentHttpService;
            _groupHttpService = groupHttpService;
            LoadGroups();
        }

        private async Task LoadGroups()
        {
            var loadedGroups = await _groupHttpService.GetAllGroups();
            Groups = new ObservableCollection<GroupModel>(loadedGroups);
        }

        public string StudentNameInput
        {
            get => _studentNameInput;
            set
            {
                _studentNameInput = value;
                OnPropertyChanged(StudentNameInput);
            }
        }

        public string StudentSurnameInput
        {
            get => _studentSurnameInput;
            set
            {
                _studentSurnameInput = value;
                OnPropertyChanged(StudentSurnameInput);
            }
        }

        public GroupModel SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        public ObservableCollection<GroupModel> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }
    }
}
