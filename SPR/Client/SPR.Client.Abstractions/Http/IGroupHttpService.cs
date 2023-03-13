using SPR.Shared.Models.Group;

namespace SPR.Client.Abstractions.Http
{
    public interface IGroupHttpService
    {
        Task<GroupModel> AddGroup(CreateGroupModel groupModel);
        Task<IReadOnlyCollection<GroupModel>> GetAllGroups();
        Task DeleteGroup(Guid id);
    }
}
