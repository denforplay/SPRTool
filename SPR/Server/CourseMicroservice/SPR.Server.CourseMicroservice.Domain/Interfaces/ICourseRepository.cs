using SPR.Server.CourseMicroservice.Domain.Models;

namespace SPR.Server.CourseMicroservice.Domain.Interfaces
{
    public interface ICourseRepository
    {
        void Add(Course course);
    }
}
