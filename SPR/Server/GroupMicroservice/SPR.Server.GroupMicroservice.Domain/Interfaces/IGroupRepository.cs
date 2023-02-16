using SPR.Server.GroupMicroservice.Domain.Models;
using System.Net.Sockets;

namespace SPR.Server.GroupMicroservice.Domain.Interfaces
{
    public interface IGroupRepository
    {
        void Add(Group group);
        Task AddAsync(Group group);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        Group ReadFirstByCondition(Func<Group, bool> condition);
        Task<Group> ReadFirstByConditionAsync(Func<Group, bool> condition);
        IReadOnlyCollection<Group> ReadAll();
        Task<IReadOnlyCollection<Group>> ReadAllAsync();
    }
}
