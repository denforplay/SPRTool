using SPR.Server.GroupMicroservice.Domain.Interfaces;

namespace SPR.Server.GroupMicroservice.Infrastructure.Services
{
    public class StudentHttpService : IStudentHttpService
    {
        private readonly HttpClient _httpClient;

        public StudentHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteAllFromGroup(Guid groupId)
        {
            var response = await _httpClient.DeleteAsync($"/Student/DeleteStudentsFromGroup?groupId={groupId}");
        }
    }
}
