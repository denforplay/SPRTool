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
            CurrentTable = new StudentTableViewModel(_studentHttpService);
            CurrentEditWindow = new StudentEditViewModel(_studentHttpService, _groupHttpService);
        }

        private void SetGroupEditMode()
        {
            CurrentTable = new GroupTableViewModel(_groupHttpService);
            CurrentEditWindow = new GroupAddViewModel(_groupHttpService);
        }
    }
}
