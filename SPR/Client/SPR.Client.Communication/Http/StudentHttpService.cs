using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Student;

namespace SPR.Client.Communication.Http
{
    public class StudentHttpService : IStudentHttpService
    {
        private readonly HttpClient _httpClient;

        public StudentHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task AddStudent(StudentModel studentModel)
        {
            return Task.FromResult(true);
        }

        public Task<StudentModel> GetAllStudents()
        {
            throw new NotImplementedException();
        }
    }
}
