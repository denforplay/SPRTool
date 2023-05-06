using Newtonsoft.Json;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Server.StudentMicroservice.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace SPR.Server.StudentMicroservice.Infrastructure.Repositories
{
    public class CourseFileRepository : ICourseRepository
    {
        private readonly string _filePath;
        private readonly FileWorker.FileWorker _fileWorker;
        private readonly List<Course> _courses;

        public CourseFileRepository(string filepath, FileWorker.FileWorker fileWorker)
        {
            _filePath = filepath;
            _fileWorker = fileWorker;

            try
            {
                _courses = _fileWorker.Read<List<Course>>(_filePath);
            }
            catch(ArgumentNullException _)
            {
                _courses = new List<Course>();
            }
        }

        public void Add(Course course)
        {
            if (!_courses.Any(x => x.Id == course.Id))
            {
                _courses.Add(course);
                Save();
            }
        }

        public async Task AddAsync(Course course)
        {
            Add(course);
        }

        public void Delete(Guid id)
        {
            _courses.RemoveAll(x => x.Id == id);
            Save();
        }

        public Task DeleteAsync(Guid id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public IReadOnlyCollection<Course> ReadAll()
        {
            return _courses;
        }

        public Task<IReadOnlyCollection<Course>> ReadAllAsync()
        {
            return Task.FromResult<IReadOnlyCollection<Course>>(_courses);
        }

        public Course ReadFirstByCondition(Func<Course, bool> condition)
        {
            return _courses.Where(condition).FirstOrDefault();
        }

        public Task<Course> ReadFirstByConditionAsync(Func<Course, bool> condition)
        {
            return Task.FromResult(ReadFirstByCondition(condition));
        }

        private void Save()
        {
            using (StreamWriter file = File.CreateText(_filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _courses);
            }
        }
    }
}