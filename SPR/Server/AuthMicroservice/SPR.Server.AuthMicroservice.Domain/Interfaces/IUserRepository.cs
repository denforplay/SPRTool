using SPR.Server.AuthMicroservice.Domain.Models;

namespace SPR.Server.AuthMicroservice.Domain.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        Task AddAsync(User user);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        User ReadFirstByCondition(Func<User, bool> condition);
        Task<User> ReadFirstByConditionAsync(Func<User, bool> condition);
        IReadOnlyCollection<User> ReadAll();
        Task<IReadOnlyCollection<User>> ReadAllAsync();
    }
}