using SPR.Shared.Models.Group;

namespace SPR.Server.StudentMicroservice.Domain.Interfaces
{
    public interface IGroupHttpService
    {
        public Task<GroupModel> ReadGroupByNameAsync(string name);
        public Task<GroupModel> ReadGroupByIdAsync(Guid id);
    }
}
