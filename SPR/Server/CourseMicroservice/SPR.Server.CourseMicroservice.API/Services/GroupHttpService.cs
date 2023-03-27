using SPR.Server.CourseMicroservice.Domain.Interfaces;
using SPR.Shared.Models.Group;

namespace SPR.Server.CourseMicroservice.API.Services
{
    public class GroupHttpService : IGroupHttpService
    {
        private readonly HttpClient _groupClient;

        public GroupHttpService(HttpClient groupClient)
        {
            _groupClient = groupClient;
        }

        public async Task<GroupModel> ReadGroupByIdAsync(Guid id)
        {
            var groupModel = await _groupClient.GetFromJsonAsync<GroupModel>($"/Group/GetGroupById?id={id.ToString()}");
            return groupModel;
        }

        public async Task<IReadOnlyCollection<GroupModel>> ReadAllGroupsAsync()
        {
            var groups = await _groupClient.GetFromJsonAsync<IReadOnlyCollection<GroupModel>>("/Group/GetAllGroups");
            return groups;
        }
    }
}
