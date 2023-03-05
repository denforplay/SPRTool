using SPR.Shared.Models.Student;

namespace SPR.Client.Abstractions.Http
{
    public interface IStudentHttpService
    {
        Task<IReadOnlyCollection<StudentModel>> GetAllStudents();
        Task AddStudent(StudentModel studentModel);
        Task DeleteStudent(Guid id);
    }
}