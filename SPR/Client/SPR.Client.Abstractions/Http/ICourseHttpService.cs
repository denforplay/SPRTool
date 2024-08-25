using SPR.Shared.Models.Course;

namespace SPR.Client.Abstractions.Http
{
    public interface ICourseHttpService
    {
        Task<CourseModel> AddCourse(CreateCourseModel courseModel);
        Task<CourseModel> UpdateCourse(UpdateCourseModel courseModel);
        Task<IReadOnlyCollection<CourseModel>> GetAllCourses();
        Task DeleteCourse(Guid id);
    }
}