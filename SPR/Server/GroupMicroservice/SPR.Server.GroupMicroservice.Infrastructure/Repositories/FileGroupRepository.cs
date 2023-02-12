using Newtonsoft.Json;
using SPR.Server.GroupMicroservice.Domain.Interfaces;
using SPR.Server.GroupMicroservice.Domain.Models;

namespace SPR.Server.GroupMicroservice.Infrastructure.Repositories
{
    public class FileGroupRepository : IGroupRepository
    {
        private List<Group> _groups;
        private readonly string _filePath;

        public FileGroupRepository(string filePath)
        {
            _groups = new List<Group>();
            _filePath = filePath;
            if (File.Exists(_filePath))
            {
                using (StreamReader file = File.OpenText(_filePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    _groups = (List<Group>)serializer.Deserialize(file, typeof(List<Group>));
                }
            }
        }

        public void Add(Group group)
        {
            if (!_groups.Any(x => x.Id == group.Id))
            {
                _groups.Add(group);
            }
        }

        public Task AddAsync(Group group)
        {
            if (!_groups.Any(x => x.Id == group.Id))
            {
                _groups.Add(group);
                Save();
            }

            return Task.CompletedTask;
        }

        public IReadOnlyCollection<Group> ReadAll()
        {
            return _groups;
        }

        public Task<IReadOnlyCollection<Group>> ReadAllAsync()
        {
            return Task.FromResult<IReadOnlyCollection<Group>>(_groups);
        }

        public Group ReadFirstByCondition(Func<Group, bool> condition)
        {
            return _groups.Where(condition).FirstOrDefault();
        }

        public Task<Group> ReadFirstByConditionAsync(Func<Group, bool> condition)
        {
            return Task.FromResult(ReadFirstByCondition(condition));
        }

        private void Save()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }

            using (StreamWriter file = File.CreateText(_filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _groups);
            }
        }
    }
}
