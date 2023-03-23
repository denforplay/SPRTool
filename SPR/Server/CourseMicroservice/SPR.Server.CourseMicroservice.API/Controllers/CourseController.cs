using Microsoft.AspNetCore.Mvc;
using SPR.Server.CourseMicroservice.Domain.Interfaces;
using SPR.Server.CourseMicroservice.Domain.Models;
using SPR.Shared.Models.Course;
using SPR.Shared.Models.Group;

namespace SPR.Server.CourseMicroservice.API.Controllers
{
    [Route("[controller]/[action]")]
    public class CourseController : Controller
    {
        private readonly IGroupHttpService _groupHttpService;
        private readonly ICourseRepository _courseRepository;

        public CourseController(IGroupHttpService groupHttpService, ICourseRepository courseRepository)
        {
            _groupHttpService = groupHttpService;
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public async Task<CourseModel> AddCourse([FromBody] CreateCourseModel courseModel)
        {
            var newCourse = new Course
            {
                Id = Guid.NewGuid(),
                Name = courseModel.Name,
                Tasks = courseModel.Tasks.Select(task => new Domain.Models.Task { Name = task.Name }).ToList(),
                Groups = courseModel.Groups is null ? new List<Guid>() : courseModel.Groups.Select(x => x.Id).ToList()
            };

            await _courseRepository.AddAsync(newCourse);

            return new CourseModel
            {
                Id = newCourse.Id,
                Name = newCourse.Name,
                Groups = courseModel.Groups,
                Tasks = courseModel.Tasks
            };
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourseById(Guid id)
        {
            await _courseRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<CourseModel>> GetAllCourses()
        {
            var courses = await _courseRepository.ReadAllAsync();
            var outputCourses = new List<CourseModel>();
            var groups = await _groupHttpService.ReadAllGroupsAsync();
            foreach (var course in courses)
            {
                List<GroupModel> courseGroups = new List<GroupModel>();

                foreach(var groupId in course.Groups)
                {
                    courseGroups.Add(groups.First(x => x.Id == groupId));
                }

                outputCourses.Add(new CourseModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    Groups = courseGroups,
                    Tasks = course.Tasks.Select(x => new TaskModel { Name = x.Name }).ToList()
                });
            }

            return outputCourses;
        }
    }
}