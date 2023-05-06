namespace SPR.Server.StudentMicroservice.Domain.Models
{
    public class StudentTask
    {
        public Guid StudentId { get; set; }
        public Guid TaskId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedTime { get; set; }
    }
}
