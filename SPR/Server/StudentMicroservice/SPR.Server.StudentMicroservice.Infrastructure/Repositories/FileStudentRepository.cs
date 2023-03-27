using Newtonsoft.Json;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Server.StudentMicroservice.Domain.Models;

namespace SPR.Server.StudentMicroservice.Infrastructure.Repositories
{
    public class FileStudentRepository : IStudentRepository
    {
        private List<Student> _students;
        private readonly FileWorker.FileWorker _fileworker;
        private readonly string _filePath;

        public FileStudentRepository(string filePath, FileWorker.FileWorker fileworker)
        {
            _fileworker = fileworker;
            _filePath = filePath;

            try
            {
                _students = _fileworker.Read<List<Student>>(_filePath);
            }
            catch (ArgumentNullException ex)
            {
                _students = new List<Student>();
            }
        }

        public void Add(Student student)
        {
            if (!_students.Any(x => x.Id == student.Id))
            {
                _students.Add(student);
                Save();
            }
        }

        public Task AddAsync(Student student)
        {
            if (!_students.Any(x => x.Id == student.Id))
            {
                _students.Add(student);
                Save();
            }

            return Task.CompletedTask;
        }

        public IReadOnlyCollection<Student> ReadAll()
        {
            return _students;
        }

        public Task<IReadOnlyCollection<Student>> ReadAllAsync()
        {
            return Task.FromResult<IReadOnlyCollection<Student>>(_students);
        }

        private void Save()
        {
            using (StreamWriter file = File.CreateText(_filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _students);
            }
        }

        public void DeleteByCondition(Func<Student, bool> condition)
        {
            _students.RemoveAll(x => condition(x));
            Save();
        }

        public Task DeleteByConditionAsync(Func<Student, bool> condition)
        {
            DeleteByCondition(condition);
            return Task.CompletedTask;
        }
    }
}