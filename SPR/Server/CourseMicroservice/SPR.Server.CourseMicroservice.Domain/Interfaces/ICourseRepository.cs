using SPR.Server.CourseMicroservice.Domain.Models;

namespace SPR.Server.CourseMicroservice.Domain.Interfaces
{
    public interface ICourseRepository
    {
        void Add(Course course);
        Task AddAsync(Course course);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        Course ReadFirstByCondition(Func<Course, bool> condition);
        Task<Course> ReadFirstByConditionAsync(Func<Course, bool> condition);
        IReadOnlyCollection<Course> ReadAll();
        Task<IReadOnlyCollection<Course>> ReadAllAsync();
    }
}