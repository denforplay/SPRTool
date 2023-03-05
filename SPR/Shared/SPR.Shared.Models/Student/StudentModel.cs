using SPR.Shared.Models.Group;

namespace SPR.Shared.Models.Student
{
    public class StudentModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public String Surname { get; set; } = null!;
        public GroupModel Group { get; set; } = null!;
    }
}
