using SPR.Server.StudentMicroservice.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace SPR.Server.StudentMicroservice.Domain.Interfaces
{
    public interface ICourseRepository
    {
        void Add(Course course);
        Task AddAsync(Course course);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Course course);
        Course ReadFirstByCondition(Func<Course, bool> condition);
        Task<Course> ReadFirstByConditionAsync(Func<Course, bool> condition);
        IReadOnlyCollection<Course> ReadAll();
        Task<IReadOnlyCollection<Course>> ReadAllAsync();
    }
}