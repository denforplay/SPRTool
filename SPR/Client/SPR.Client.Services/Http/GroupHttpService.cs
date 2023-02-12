using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Group;

namespace SPR.Client.Services.Http
{
    public class GroupHttpService : IGroupHttpService
    {
        public IReadOnlyCollection<GroupModel> GetAllGroups()
        {
            throw new NotImplementedException();
        }
    }
}