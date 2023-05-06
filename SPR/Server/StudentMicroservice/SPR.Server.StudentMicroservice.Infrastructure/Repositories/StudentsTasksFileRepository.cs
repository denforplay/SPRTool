using Newtonsoft.Json;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Server.StudentMicroservice.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace SPR.Server.StudentMicroservice.Infrastructure.Repositories
{
    public class StudentsTasksFileRepository : IStudentsTasksRepository
    {
        private List<StudentTask> _studentsTasks;
        private readonly FileWorker.FileWorker _fileworker;
        private readonly string _filePath;

        public StudentsTasksFileRepository(string filePath, FileWorker.FileWorker fileWorker)
        {
            _fileworker = fileWorker;
            _filePath = filePath;

            try
            {
                _studentsTasks = _fileworker.Read<List<StudentTask>>(_filePath);
            }
            catch (ArgumentNullException ex)
            {
                _studentsTasks = new List<StudentTask>();
            }
        }

        public void Add(StudentTask studentTask)
        {
            if (!_studentsTasks.Any(x => x.StudentId == studentTask.StudentId && x.TaskId == studentTask.TaskId))
            {
                _studentsTasks.Add(studentTask);
                Save();
            }
        }

        public Task AddAsync(StudentTask studentTask)
        {
            Add(studentTask);
            return Task.CompletedTask;
        }

        public void DeleteByCondition(Func<StudentTask, bool> condition)
        {
            _studentsTasks.RemoveAll(x => condition(x));
            Save();
        }

        public Task DeleteByConditionAsync(Func<StudentTask, bool> condition)
        {
            DeleteByCondition(condition);
            return Task.CompletedTask;
        }

        public void Update(StudentTask studentTask)
        {
            _studentsTasks.RemoveAll(x => x.StudentId == studentTask.StudentId && x.TaskId == studentTask.TaskId);
            _studentsTasks.Add(studentTask);
            Save();
        }

        public Task UpdateAsync(StudentTask studentTask)
        {
            Update(studentTask);
            return Task.CompletedTask;
        }


        private void Save()
        {
            using (StreamWriter file = File.CreateText(_filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _studentsTasks);
            }
        }

        public IReadOnlyCollection<StudentTask> ReadAll()
        {
            return _studentsTasks;
        }

        public async Task<IReadOnlyCollection<StudentTask>> ReadAllAsync()
        {
            return _studentsTasks;
        }
    }
}