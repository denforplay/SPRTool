using SPR.Shared.Models.StudentTask;

namespace SPR.Client.Abstractions.Http
{
    public interface IStudentTaskHttpService
    {
        Task<StudentTaskModel> AddStudentTask(CreateStudentTaskModel createStudentTaskModel);
        Task<StudentTaskModel> UpdateStudentTask(CreateStudentTaskModel updateStudentTaskModel);
        Task<StudentTaskModel> GetInfoAboutStudentTask(Guid studentId, Guid taskId);
    }
}