using SPR.Shared.Models.Group;

namespace SPR.Shared.Models.Course
{
    public class UpdateCourseModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; } = null!;
        public IReadOnlyCollection<GroupModel> Groups { get; set; }
        public IReadOnlyCollection<CreateTaskModel> Tasks { get; set; }
    }
}
