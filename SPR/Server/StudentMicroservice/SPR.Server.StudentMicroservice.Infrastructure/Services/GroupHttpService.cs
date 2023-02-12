using Newtonsoft.Json;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Shared.Models.Group;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;

namespace SPR.Server.StudentMicroservice.Infrastructure.Services
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

        public async Task<GroupModel> ReadGroupByNameAsync(string name)
        {
            var groupModel = await _groupClient.GetFromJsonAsync<GroupModel>($"/Group/GetGroupByName?name={name}");
            return groupModel;
        }
    }
}
