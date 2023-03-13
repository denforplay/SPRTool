namespace SPR.Server.GroupMicroservice.Domain.Interfaces
{
    public interface IStudentHttpService
    {
        Task DeleteAllFromGroup(Guid groupId);
    }
}
