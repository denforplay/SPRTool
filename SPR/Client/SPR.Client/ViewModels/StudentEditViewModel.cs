using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Group;
using SPR.Shared.Models.Student;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace SPR.Client.ViewModels
{
    public class StudentEditViewModel : ViewModelBase
    {
        public event Action<StudentModel> OnStudentAdded;

        public CommandBase AddStudentCommand { get; }
        public CommandBase OpenAddGroupMenuCommand { get; }
        public CommandBase CloseAddGroupMenuCommand { get; }
        public CommandBase AddGroupFromMenuCommand { get; }
        private readonly IStudentHttpService _studentHttpService;
        private readonly IGroupHttpService _groupHttpService;
        private bool _isAddGroupMenuOpened;
        private string _newGroupName;
        private string _studentNameInput;
        private string _studentSurnameInput;
        private GroupModel _selectedGroup;
        private ObservableCollection<GroupModel> _groups;

        public StudentEditViewModel(IStudentHttpService studentHttpService, IGroupHttpService groupHttpService)
        {
            _studentHttpService = studentHttpService;
            _groupHttpService = groupHttpService;
            AddStudentCommand = new ActionCommand(() => Application.Current.Dispatcher.Invoke(async () => await AddStudentAsync()), CanAddStudent);
            OpenAddGroupMenuCommand = new ActionCommand(OpenAddGroupMenu, CanOpenAddGroupMenu);
            CloseAddGroupMenuCommand = new ActionCommand(CloseAddGroupMenu, CanCloseAddGroupMenu);
            AddGroupFromMenuCommand = new ActionCommand(() => Application.Current.Dispatcher.Invoke(async () => await AddGroupFromMenu()), CanAddGroupFromMenu);
            Application.Current.Dispatcher.Invoke(async () => await LoadGroupsAndStudents());
        }

        private async Task AddGroupFromMenu()
        {
            var createGroupModel = new CreateGroupModel
            {
                Name = NewGroupName
            };

            var result = await _groupHttpService.AddGroup(createGroupModel);
            try
            {
                Groups.Add(result);
            }
            catch(Exception ex)
            {

            }
            SelectedGroup = result;
            CloseAddGroupMenuCommand.Execute(null);
        }

        private bool CanAddGroupFromMenu()
        {
            return _isAddGroupMenuOpened && !string.IsNullOrEmpty(NewGroupName);
        }

        private void CloseAddGroupMenu()
        {
            IsAddGroupMenuOpened = false;
        }

        private bool CanCloseAddGroupMenu()
        {
            return _isAddGroupMenuOpened;
        }

        private void OpenAddGroupMenu()
        {
            IsAddGroupMenuOpened = true;
        }

        private bool CanOpenAddGroupMenu()
        {
            return !_isAddGroupMenuOpened;
        }

        private bool CanAddStudent()
        {
            return !String.IsNullOrEmpty(StudentNameInput)
                && !String.IsNullOrEmpty(StudentSurnameInput)
                && SelectedGroup is not null;
        }

        private async Task AddStudentAsync()
        {
            var studentModel = new StudentCreateModel
            {
                Name = StudentNameInput,
                Surname = StudentSurnameInput,
                Group = SelectedGroup,
            };

            var createdModel = await _studentHttpService.AddStudent(studentModel);
            OnStudentAdded?.Invoke(createdModel);
        }

        private async Task LoadGroupsAndStudents()
        {
            var loadedGroups = await _groupHttpService.GetAllGroups();
            Groups = new ObservableCollection<GroupModel>(loadedGroups);
        }

        public bool IsAddGroupMenuOpened
        {
            get => _isAddGroupMenuOpened;
            set
            {
                _isAddGroupMenuOpened = value;
                OnPropertyChanged(nameof(IsAddGroupMenuOpened));
                CloseAddGroupMenuCommand.RaiseExecuteChanged();
                OpenAddGroupMenuCommand.RaiseExecuteChanged();
            }
        }

        public String NewGroupName
        {
            get => _newGroupName;
            set
            {
                _newGroupName = value;
                OnPropertyChanged(nameof(NewGroupName));
                AddGroupFromMenuCommand.RaiseExecuteChanged();
            }
        }

        public string StudentNameInput
        {
            get => _studentNameInput;
            set
            {
                _studentNameInput = value;
                OnPropertyChanged(StudentNameInput);
                AddStudentCommand.RaiseExecuteChanged();
            }
        }

        public string StudentSurnameInput
        {
            get => _studentSurnameInput;
            set
            {
                _studentSurnameInput = value;
                OnPropertyChanged(StudentSurnameInput);
                AddStudentCommand.RaiseExecuteChanged();
            }
        }

        public GroupModel SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
                AddStudentCommand.RaiseExecuteChanged();
            }
        }

        public ObservableCollection<GroupModel> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
                AddStudentCommand.RaiseExecuteChanged();
            }
        }
    }
}
