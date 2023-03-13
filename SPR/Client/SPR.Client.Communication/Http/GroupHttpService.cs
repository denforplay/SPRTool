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

        public async Task<GroupModel> AddGroup(CreateGroupModel groupModel)
        {
            var result = await _groupClient.PostAsJsonAsync<CreateGroupModel>($"/Group/AddGroup", groupModel);
            var createdModel = await result.Content.ReadFromJsonAsync<GroupModel>();
            return createdModel;
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
