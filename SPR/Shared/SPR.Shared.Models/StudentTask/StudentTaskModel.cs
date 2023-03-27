namespace SPR.Shared.Models.StudentTask
{
    public class StudentTaskModel
    {
        public Guid StudentId { get; set; }
        public Guid TaskId { get; set; }
        public bool IsCompleted { get; set; }
    }
}