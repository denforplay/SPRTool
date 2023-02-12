using System.ComponentModel.DataAnnotations;

namespace SPR.Server.StudentMicroservice.Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        [Required]
        public String Name { get; set; } = null!;

        [Required]
        public String Surname { get; set; } = null!;

        [Required]
        public Guid GroupId { get; set; }
    }
}
