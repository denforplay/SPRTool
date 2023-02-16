using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Group;
using System.Net.Http.Json;

namespace SPR.Client.Communication.Http
{
    public class GroupHttpService : IGroupHttpService
    {
        private readonly HttpClient _groupClient;

        public GroupHttpService(HttpClient groupClient)
        {
            _groupClient = groupClient;
        }

        public async Task AddGroup(GroupModel groupModel)
        {
            var result = await _groupClient.PostAsJsonAsync<GroupModel>($"/Group/AddGroup", groupModel);
        }

        public async Task DeleteGroup(Guid id)
        {
            await _groupClient.DeleteAsync($"/Group/DeleteGroupById?id={id}");
        }

        public async Task<IReadOnlyCollection<GroupModel>> GetAllGroups()
        {
            var groups = await _groupClient.GetFromJsonAsync<IReadOnlyCollection<GroupModel>>($"/Group/GetAllGroups");
            return groups;
        }
    }
}
