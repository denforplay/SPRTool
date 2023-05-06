using Microsoft.AspNetCore.Mvc;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Server.StudentMicroservice.Domain.Models;
using SPR.Shared.Models.StudentTask;

namespace SPR.Server.StudentMicroservice.API.Controllers
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
                IsCompleted = createStudentTaskModel.IsCompleted,
                CompletedTime = createStudentTaskModel.CompletedTime
            };

            await _studentsTasksRepository.UpdateAsync(createdModel);

            return new StudentTaskModel
            {
                StudentId = createStudentTaskModel.StudentId,
                TaskId = createStudentTaskModel.TaskId,
                IsCompleted = createStudentTaskModel.IsCompleted,
                CompletedTime = createStudentTaskModel.CompletedTime
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
        public async System.Threading.Tasks.Task DeleteAllTasksForStudent(Guid studentId)
        {
            await _studentsTasksRepository.DeleteByConditionAsync(studentTask => studentTask.StudentId == studentId);
        }
    }
}