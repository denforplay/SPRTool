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

        public async Task<StudentModel> AddStudent(StudentCreateModel studentModel)
        {
            var result = await _studentClient.PostAsJsonAsync<StudentCreateModel>($"/Student/AddStudent", studentModel);
            var createdStudentModel = await result.Content.ReadFromJsonAsync<StudentModel>();
            return createdStudentModel;
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