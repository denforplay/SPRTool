using SPR.Server.StudentMicroservice.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace SPR.Server.StudentMicroservice.Domain.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        Task AddAsync(Student student);
        void DeleteByCondition(Func<Student, bool> condition);
        Task DeleteByConditionAsync(Func<Student, bool> condition);
        IReadOnlyCollection<Student> ReadAll();
        Task<IReadOnlyCollection<Student>> ReadAllAsync();
    }
}