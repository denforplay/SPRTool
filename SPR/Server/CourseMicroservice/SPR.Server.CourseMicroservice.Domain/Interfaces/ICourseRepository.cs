using SPR.Server.CourseMicroservice.Domain.Models;

namespace SPR.Server.CourseMicroservice.Domain.Interfaces
{
    internal interface ICourseRepository
    {
        void Add(Course course);
    }
}
