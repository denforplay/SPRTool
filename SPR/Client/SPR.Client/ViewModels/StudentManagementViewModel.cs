using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using System.Windows.Input;

namespace SPR.Client.ViewModels
{
    public class StudentManagementViewModel : ViewModelBase
    {
        private readonly IGroupHttpService _groupHttpService;
        private readonly IStudentHttpService _studentHttpService;

        public ICommand StudentModeCommand { get; }
        public ICommand GroupModeCommand { get; }
        private ViewModelBase _currentTable;
        private ViewModelBase _currentEditWindow;
        
        public ViewModelBase CurrentTable
        { 
            get => _currentTable; 
            set
            {
                _currentTable = value;
                OnPropertyChanged(nameof(CurrentTable));
            }
        }

        public ViewModelBase CurrentEditWindow
        {
            get => _currentEditWindow;
            set
            {
                _currentEditWindow = value;
                OnPropertyChanged(nameof(CurrentEditWindow));
            }
        }

        public StudentManagementViewModel(IStudentHttpService studentHttpService, IGroupHttpService groupHttpService)
        {
            _groupHttpService = groupHttpService;
            _studentHttpService = studentHttpService;
            StudentModeCommand = new ActionCommand(SetStudentEditMode);
            GroupModeCommand = new ActionCommand(SetGroupEditMode);
        }

        private void SetStudentEditMode()
        {
            var currentTable = new StudentTableViewModel(_studentHttpService);
            var currentEditWindow = new StudentEditViewModel(_studentHttpService, _groupHttpService);
            currentEditWindow.OnStudentAdded += createdStudent => currentTable.Students.Add(createdStudent);
            CurrentTable = currentTable;
            CurrentEditWindow = currentEditWindow;
        }

        private void SetGroupEditMode()
        {
            var currentTable = new GroupTableViewModel(_groupHttpService);
            var currentEditWindow = new GroupAddViewModel(_groupHttpService);
            currentEditWindow.OnGroupAdded += createdGroup => currentTable.Groups.Add(createdGroup);
            CurrentTable = currentTable;
            CurrentEditWindow = currentEditWindow;
        }
    }
}
