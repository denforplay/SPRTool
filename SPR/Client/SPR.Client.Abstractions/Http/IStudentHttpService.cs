using SPR.Shared.Models.Student;

namespace SPR.Client.Abstractions.Http
{
    public interface IStudentHttpService
    {
        Task<IReadOnlyCollection<StudentModel>> GetAllStudents();
        Task<IReadOnlyCollection<StudentModel>> GetAllStudentsFromGroup(Guid groupId);
        Task<StudentModel> AddStudent(StudentCreateModel studentModel);
        Task DeleteStudent(Guid id);
    }
}