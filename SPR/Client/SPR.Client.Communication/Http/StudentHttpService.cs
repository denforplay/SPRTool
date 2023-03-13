using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Student;
using System.Net.Http.Json;

namespace SPR.Client.Communication.Http
{
    public class StudentHttpService : IStudentHttpService
    {
        private readonly HttpClient _studentClient;

        public StudentHttpService(HttpClient studentClient)
        {
            _studentClient = studentClient;
        }

        public async Task AddStudent(StudentModel studentModel)
        {
            var result = await _studentClient.PostAsJsonAsync<StudentModel>($"/Student/AddStudent", studentModel);
        }

        public async Task DeleteStudent(Guid id)
        {
            await _studentClient.DeleteAsync($"/Student/DeleteStudentById?id={id}");
        }

        public async Task<IReadOnlyCollection<StudentModel>> GetAllStudents()
        {
            var students = await _studentClient.GetFromJsonAsync<IReadOnlyCollection<StudentModel>>($"/Student/GetAllStudents");
            return students;
        }
    }
}