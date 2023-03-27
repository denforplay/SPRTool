using SPR.Shared.Models.Group;

namespace SPR.Server.CourseMicroservice.Domain.Interfaces
{
    public interface IGroupHttpService
    {
        public Task<GroupModel> ReadGroupByIdAsync(Guid id);
        public Task<IReadOnlyCollection<GroupModel>> ReadAllGroupsAsync();
    }
}
