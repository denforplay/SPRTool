using SPR.Server.StudentMicroservice.Domain.Interfaces;

namespace SPR.Server.StudentMicroservice.Infrastructure.Services
{
    public class StudentTaskHttpService : IStudentTaskHttpService
    {
        private readonly HttpClient _studentTaskClient;

        public StudentTaskHttpService(HttpClient studentTaskClient)
        {
            _studentTaskClient = studentTaskClient;
        }

        public async Task DeleteAllTasksForStudent(Guid studentId)
        {
            var response = await _studentTaskClient.DeleteAsync($"/StudentsTasks/DeleteAllTasksForStudent?studentId={studentId}");
        }
    }
}
