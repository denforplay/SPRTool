using SPR.Shared.Models.Group;

namespace SPR.Shared.Models.Course
{
    public class CreateCourseModel
    {
        public String Name { get; set; } = null!;
        public IReadOnlyCollection<GroupModel> Groups { get; set; }
    }
}
