using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Group;
using SPR.Shared.Models.Student;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SPR.Client.ViewModels
{
    public class StudentEditViewModel : ViewModelBase
    {
        public ActionCommand AddStudentCommand { get; }
        public ActionCommand AddGroupCommand { get; }
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
            AddStudentCommand = new ActionCommand(() => Task.Run(async () => await AddStudentAsync()), CanAddStudent);
            Task.Run(async () => await LoadGroupsAndStudents());
        }

        private bool CanAddStudent()
        {
            return !String.IsNullOrEmpty(StudentNameInput)
                && !String.IsNullOrEmpty(StudentSurnameInput)
                && SelectedGroup is not null;
        }

        private async Task AddStudentAsync()
        {
            var studentModel = new StudentModel
            {
                Name = StudentNameInput,
                Surname = StudentSurnameInput,
                Group = SelectedGroup,
            };


            await _studentHttpService.AddStudent(studentModel);
        }

        private async Task LoadGroupsAndStudents()
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
