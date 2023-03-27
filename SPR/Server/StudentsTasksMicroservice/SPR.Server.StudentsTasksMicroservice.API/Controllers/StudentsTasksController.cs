using Microsoft.AspNetCore.Mvc;
using SPR.Server.StudentsTasksMicroservice.Domain.Interfaces;
using SPR.Server.StudentsTasksMicroservice.Domain.Models;
using SPR.Shared.Models.StudentTask;

namespace SPR.Server.StudentsTasksMicroservice.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentsTasksController : ControllerBase
    {
        private readonly IStudentsTasksRepository _studentsTasksRepository;

        public StudentsTasksController(IStudentsTasksRepository studentsTasksRepository)
        {
            _studentsTasksRepository = studentsTasksRepository;
        }

        [HttpPost]
        public async Task<StudentTaskModel> AddStudentTask(CreateStudentTaskModel createStudentTaskModel)
        {
            var createdModel = new StudentTask
            {
                StudentId = createStudentTaskModel.StudentId,
                TaskId = createStudentTaskModel.TaskId,
                IsCompleted = createStudentTaskModel.IsCompleted
            };

            await _studentsTasksRepository.AddAsync(createdModel);

            return new StudentTaskModel
            {
                StudentId = createStudentTaskModel.StudentId,
                TaskId = createStudentTaskModel.TaskId,
                IsCompleted = createStudentTaskModel.IsCompleted
            };
        }

        [HttpPut]
        public async Task<StudentTaskModel> UpdateStudentTask(CreateStudentTaskModel createStudentTaskModel)
        {
            var createdModel = new StudentTask
            {
                StudentId = createStudentTaskModel.StudentId,
                TaskId = createStudentTaskModel.TaskId,
                IsCompleted = createStudentTaskModel.IsCompleted
            };

            await _studentsTasksRepository.UpdateAsync(createdModel);

            return new StudentTaskModel
            {
                StudentId = createStudentTaskModel.StudentId,
                TaskId = createStudentTaskModel.TaskId,
                IsCompleted = createStudentTaskModel.IsCompleted
            };
        }

        [HttpGet]
        public async Task<StudentTaskModel> GetInfoAboutStudentTask(Guid studentId, Guid taskId)
        {
            var newStudentTaskModel = new StudentTaskModel
            {
                StudentId = studentId,
                TaskId = taskId,
                IsCompleted = false
            };

            var findedStudentTask = (await _studentsTasksRepository.ReadAllAsync()).FirstOrDefault(x => x.StudentId == studentId && x.TaskId == taskId);

            if (findedStudentTask is not null)
            {
                newStudentTaskModel.IsCompleted = findedStudentTask.IsCompleted;
            }

            return newStudentTaskModel;
        }

        [HttpDelete]
        public async Task DeleteAllTasksForStudent(Guid studentId)
        {
            await _studentsTasksRepository.DeleteByConditionAsync(studentTask => studentTask.StudentId == studentId);
        }
    }
}