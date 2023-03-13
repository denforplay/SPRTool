using Newtonsoft.Json;
using SPR.Server.AuthMicroservice.Domain.Interfaces;
using SPR.Server.AuthMicroservice.Domain.Models;

namespace SPR.Server.AuthMicroservice.Infrastructure.Repositories
{
    public class UserFileRepository : IUserRepository
    {
        private readonly string _filePath;
        private readonly FileWorker.FileWorker _fileWorker;
        private readonly List<User> _users;

        public UserFileRepository(string filePath, FileWorker.FileWorker fileWorker)
        {
            _filePath = filePath;
            _fileWorker = fileWorker;

            try
            {
                _users = _fileWorker.Read<List<User>>(_filePath);
            }
            catch (ArgumentNullException ex)
            {
                _users = new List<User>();
            }
        }

        public void Add(User user)
        {
            if (!_users.Any(x => x.Id == user.Id))
            {
                _users.Add(user);
                Save();
            }
        }

        public Task AddAsync(User user)
        {
            return Task.FromResult(Add);
        }

        public void Delete(Guid id)
        {
            _users.RemoveAll(x => x.Id == id);
            Save();
        }

        public Task DeleteAsync(Guid id)
        {
            Delete(id);
            return Task.CompletedTask;
        }

        public IReadOnlyCollection<User> ReadAll()
        {
            return _users;
        }

        public Task<IReadOnlyCollection<User>> ReadAllAsync()
        {
            return Task.FromResult<IReadOnlyCollection<User>>(_users);
        }

        public User ReadFirstByCondition(Func<User, bool> condition)
        {
            return _users.Where(condition).FirstOrDefault();
        }

        public Task<User> ReadFirstByConditionAsync(Func<User, bool> condition)
        {
            return Task.FromResult(ReadFirstByCondition(condition));
        }

        private void Save()
        {
            using (StreamWriter file = File.CreateText(_filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _users);
            }
        }
    }
}
