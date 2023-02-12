using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Student;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SPR.Client.ViewModels
{
    public class StudentTableViewModel : ViewModelBase
    {
        private readonly IStudentHttpService _studentHttpService;
        private ObservableCollection<StudentModel> _studentsModel;

        public StudentTableViewModel(IStudentHttpService studentHttpService)
        {
            _studentHttpService = studentHttpService;
        }

        public async Task LoadStudents()
        {
        }

        public ObservableCollection<StudentModel> Students => _studentsModel;
    }
}
