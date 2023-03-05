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
        private readonly IGroupHttpService _groupHttpService;

        public StudentController(IStudentRepository studentRepository, IGroupHttpService groupHttpService)
        {
            _studentRepository = studentRepository;
            _groupHttpService = groupHttpService;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentModel studentModel)
        {
            await _studentRepository.AddAsync(new Student
            {
                Id = Guid.NewGuid(),
                GroupId = studentModel.Group.Id,
                Name = studentModel.Name,
                Surname = studentModel.Surname,
            });

            return Ok();
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<StudentModel>> GetAllStudents()
        {
            var students = await _studentRepository.ReadAllAsync();
            var outputStudents = new List<StudentModel>();

            foreach(var student in students)
            {
                outputStudents.Add(new StudentModel()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Surname = student.Surname,
                    Group = await _groupHttpService.ReadGroupByIdAsync(student.GroupId)
                });
            }

            return outputStudents;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudentById(Guid id)
        {
            await _studentRepository.DeleteAsync(id);
            return Ok();
        }
    }
}