using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Server.StudentMicroservice.Domain.Models;

namespace SPR.Server.StudentMicroservice.Infrastructure.Repositories
{
    public class FileStudentRepository : IStudentRepository
    {
        private List<Student> _students;
        private readonly string _filePath;

        public FileStudentRepository(string filePath)
        {
            _students = new List<Student>();
            _filePath = filePath;
        }

        public void Add(Student student)
        {
            if (!_students.Any(x => x.Id == student.Id))
            {
                _students.Add(student);
            }
        }

        public Task AddAsync(Student student)
        {
            if (!_students.Any(x => x.Id == student.Id))
            {
                _students.Add(student);
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
    }
}
