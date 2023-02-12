using SPR.Shared.Models.Student;

namespace SPR.Client.Abstractions.Http
{
    public interface IStudentHttpService
    {
        Task<StudentModel> GetAllStudents();
        Task AddStudent(StudentModel studentModel);
    }
}
