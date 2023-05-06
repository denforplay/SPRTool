using Microsoft.AspNetCore.Mvc;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Server.StudentMicroservice.Domain.Models;
using SPR.Shared.Models.Course;
using SPR.Shared.Models.Group;

namespace SPR.Server.StudentMicroservice.API.Controllers
{
    [Route("[controller]/[action]")]
    public class CourseController : Controller
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseController(IGroupRepository groupRepository, ICourseRepository courseRepository)
        {
            _groupRepository = groupRepository;
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public async Task<CourseModel> AddCourse([FromBody] CreateCourseModel courseModel)
        {
            var newCourse = new Course
            {
                Id = Guid.NewGuid(),
                Name = courseModel.Name,
                Tasks = courseModel.Tasks.Select(task => new Domain.Models.Task { Id = Guid.NewGuid(), Name = task.Name }).ToList(),
                Groups = courseModel.Groups is null ? new List<Guid>() : courseModel.Groups.Select(x => x.Id).ToList()
            };

            await _courseRepository.AddAsync(newCourse);

            return new CourseModel
            {
                Id = newCourse.Id,
                Name = newCourse.Name,
                Groups = courseModel.Groups,
                Tasks = newCourse.Tasks.Select(task => new TaskModel
                {
                    Id = task.Id,
                    Name = task.Name
                }).ToList()
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
            var groups = await _groupRepository.ReadAllAsync();
            foreach (var course in courses)
            {
                List<GroupModel> courseGroups = new List<GroupModel>();

                foreach(var groupId in course.Groups)
                {
                    var group = groups.First(x => x.Id == groupId);
                    courseGroups.Add(new GroupModel
                    {
                        Id = group.Id,
                        Name = group.Name
                    });
                }

                outputCourses.Add(new CourseModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    Groups = courseGroups,
                    Tasks = course.Tasks.Select(x => new TaskModel { Id = x.Id, Name = x.Name }).ToList()
                });
            }

            return outputCourses;
        }
    }
}