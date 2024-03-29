﻿namespace SPR.Server.StudentMicroservice.Domain.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Guid> Groups { get; set; } = null!;
        public ICollection<Task> Tasks { get; set; }
    }
}