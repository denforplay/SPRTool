using SPR.Server.StudentMicroservice.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace SPR.Server.StudentMicroservice.Domain.Interfaces
{
    public interface IStudentsTasksRepository
    {
        void Add(StudentTask studentTask);
        Task AddAsync(StudentTask studentTask);
        void Update(StudentTask studentTask);
        Task UpdateAsync(StudentTask studentTask);
        void DeleteByCondition(Func<StudentTask, bool> condition);
        Task DeleteByConditionAsync(Func<StudentTask, bool> condition);
        IReadOnlyCollection<StudentTask> ReadAll();
        Task<IReadOnlyCollection<StudentTask>> ReadAllAsync();
    }
}