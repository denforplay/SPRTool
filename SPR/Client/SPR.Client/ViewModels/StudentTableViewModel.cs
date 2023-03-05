using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Student;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SPR.Client.ViewModels
{
    public class StudentTableViewModel : ViewModelBase
    {
        private readonly IStudentHttpService _studentHttpService;
        private IReadOnlyCollection<StudentModel> _studentsModel;
        private StudentModel _selectedStudent;

        public ActionCommand DeleteCommand { get; }

        public StudentTableViewModel(IStudentHttpService studentHttpService)
        {
            _studentHttpService = studentHttpService;
            Task.Run(async () => await LoadStudents());
            DeleteCommand = new ActionCommand(() => Task.Run(async() => await DeleteStudent()));
        }

        public async Task LoadStudents()
        {
            var response = await _studentHttpService.GetAllStudents();
            Students = response;
        }

        private async Task DeleteStudent()
        {
            await _studentHttpService.DeleteStudent(SelectedStudent.Id);
        }

        public IReadOnlyCollection<StudentModel> Students
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
