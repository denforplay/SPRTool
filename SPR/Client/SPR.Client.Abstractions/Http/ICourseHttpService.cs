using SPR.Shared.Models.Course;
using SPR.Shared.Models.Group;

namespace SPR.Client.Abstractions.Http
{
    public interface ICourseHttpService
    {
        Task<CourseModel> AddCourse(CreateCourseModel courseModel);
        Task<IReadOnlyCollection<CourseModel>> GetAllCourses();
        Task DeleteCourse(Guid id);
    }
}