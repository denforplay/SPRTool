using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.StudentTask;
using System.Net.Http.Json;

namespace SPR.Client.Communication.Http
{
    public class StudentTaskHttpService : IStudentTaskHttpService
    {
        private readonly HttpClient _studentTaskClient;

        public StudentTaskHttpService(HttpClient studentTaskClient)
        {
            _studentTaskClient = studentTaskClient;
        }

        public async Task<StudentTaskModel> AddStudentTask(CreateStudentTaskModel createStudentTaskModel)
        {
            var response = await _studentTaskClient.PostAsJsonAsync<CreateStudentTaskModel>($"/StudentsTasks/AddStudent", createStudentTaskModel);
            var createdModel = await response.Content.ReadFromJsonAsync<StudentTaskModel>();
            return createdModel;
        }

        public async Task<StudentTaskModel> GetInfoAboutStudentTask(Guid studentId, Guid taskId)
        {
            var response = await _studentTaskClient.GetFromJsonAsync<StudentTaskModel>($"/StudentsTasks/GetInfoAboutStudentTask?studentId={studentId}&taskId={taskId}");
            return response;
        }

        public async Task<StudentTaskModel> UpdateStudentTask(CreateStudentTaskModel updateStudentTaskModel)
        {
            var response = await _studentTaskClient.PutAsJsonAsync<CreateStudentTaskModel>($"/StudentsTasks/UpdateStudentTask", updateStudentTaskModel);
            var updatedModel = await response.Content.ReadFromJsonAsync<StudentTaskModel>();
            return updatedModel;
        }
    }
}
