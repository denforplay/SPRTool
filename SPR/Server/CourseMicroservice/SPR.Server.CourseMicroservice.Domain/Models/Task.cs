namespace SPR.Server.CourseMicroservice.Domain.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}