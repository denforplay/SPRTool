using SPR.Server.StudentMicroservice.Domain.Models;

namespace SPR.Server.StudentMicroservice.Domain.Interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        Task AddAsync(Student student);
        void Delete(Guid id);
        Task DeleteAsync(Guid id);
        IReadOnlyCollection<Student> ReadAll();
        Task<IReadOnlyCollection<Student>> ReadAllAsync();
    }
}