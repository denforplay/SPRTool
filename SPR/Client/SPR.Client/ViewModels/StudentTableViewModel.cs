using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Student;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SPR.Client.ViewModels
{
    public class StudentTableViewModel : ViewModelBase
    {
        private readonly IStudentHttpService _studentHttpService;
        private ObservableCollection<StudentModel> _studentsModel;
        private StudentModel _selectedStudent;

        public ActionCommand DeleteCommand { get; }

        public StudentTableViewModel(IStudentHttpService studentHttpService)
        {
            _studentHttpService = studentHttpService;
            Application.Current.Dispatcher.Invoke(async () => await LoadStudents());
            DeleteCommand = new ActionCommand(() => Application.Current.Dispatcher.Invoke(async() => await DeleteStudent()));
        }

        public async Task LoadStudents()
        {
            var response = await _studentHttpService.GetAllStudents();
            Students = new ObservableCollection<StudentModel>(response);
        }

        private async Task DeleteStudent()
        {
            await _studentHttpService.DeleteStudent(SelectedStudent.Id);
        }

        public ObservableCollection<StudentModel> Students
        {
            get => _studentsModel;
            set
            {
                _studentsModel = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        public StudentModel SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));
            }
        }
    }
}
