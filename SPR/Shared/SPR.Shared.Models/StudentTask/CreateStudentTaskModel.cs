namespace SPR.Shared.Models.StudentTask
{
    public class CreateStudentTaskModel
    {
        public Guid StudentId { get; set; }
        public Guid TaskId { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedTime { get; set; }
    }
}