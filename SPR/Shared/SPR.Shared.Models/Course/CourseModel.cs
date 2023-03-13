using SPR.Shared.Models.Group;

namespace SPR.Shared.Models.Course
{
    public class CourseModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public GroupModel? Groups { get; set; }
    }
}