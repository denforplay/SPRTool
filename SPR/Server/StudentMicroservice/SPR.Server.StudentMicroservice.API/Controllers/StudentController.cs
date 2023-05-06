using Microsoft.AspNetCore.Mvc;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Server.StudentMicroservice.Domain.Models;
using SPR.Shared.Models.Student;

namespace SPR.Server.StudentMicroservice.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentsTasksRepository _studentTasksRepository;

        public StudentController(IStudentRepository studentRepository, 
            IGroupRepository groupRepository,
            IStudentsTasksRepository studentsTasksRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _studentTasksRepository = studentsTasksRepository;
        }

        [HttpPost]
        public async Task<StudentModel> AddStudent(StudentCreateModel studentModel)
        {
            var createdStudent = new Student
            {
                Id = Guid.NewGuid(),
                GroupId = studentModel.Group.Id,
                Name = studentModel.Name,
                Surname = studentModel.Surname,
            };

            await _studentRepository.AddAsync(createdStudent);

            return new StudentModel 
            {
                Id = createdStudent.Id,
                Group = studentModel.Group,
                Name = createdStudent.Name,
                Surname = createdStudent.Surname
            };
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<StudentModel>> GetAllStudents()
        {
            var students = await _studentRepository.ReadAllAsync();
            var outputStudents = new List<StudentModel>();

            foreach(var student in students)
            {
                var group = await _groupRepository.ReadFirstByConditionAsync(x => x.Id == student.GroupId);

                outputStudents.Add(new StudentModel()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Group = new Shared.Models.Group.GroupModel
                    {
                        Id = group.Id,
                        Name = group.Name
                    }
                });
            }

            return outputStudents;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<StudentModel>> GetAllStudentsFromGroup(Guid groupId)
        {
            var students = (await _studentRepository.ReadAllAsync()).Where(x => x.GroupId == groupId);
            var outputStudents = new List<StudentModel>();

            foreach (var student in students)
            {
                var group = await _groupRepository.ReadFirstByConditionAsync(x => x.Id == student.GroupId);

                outputStudents.Add(new StudentModel()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Group = new Shared.Models.Group.GroupModel
                    {
                        Id = group.Id,
                        Name = group.Name
                    }
                });
            }

            return outputStudents;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudentById(Guid id)
        {
            await _studentRepository.DeleteByConditionAsync(x => x.Id == id);
            await _studentTasksRepository.DeleteByConditionAsync(x => x.StudentId == id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudentsFromGroup(Guid groupId)
        {
            var studentsFromGroup = (await _studentRepository.ReadAllAsync()).Where(x => x.GroupId == groupId);
            await _studentRepository.DeleteByConditionAsync(x => x.GroupId == groupId);
            foreach(var studentFromGroup in studentsFromGroup)
            {
                await _studentTasksRepository.DeleteByConditionAsync(x => x.StudentId == studentFromGroup.Id);
            }

            return Ok();
        }
    }
}