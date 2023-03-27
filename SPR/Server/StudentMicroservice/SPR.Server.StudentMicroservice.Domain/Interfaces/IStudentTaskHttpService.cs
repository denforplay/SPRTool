namespace SPR.Server.StudentMicroservice.Domain.Interfaces
{
    public interface IStudentTaskHttpService
    {
        Task DeleteAllTasksForStudent(Guid studentId);
    }
}
