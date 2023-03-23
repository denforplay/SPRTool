namespace SPR.Server.CourseMicroservice.Domain.Models
{
    public class Task
    {
        public string Name { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
