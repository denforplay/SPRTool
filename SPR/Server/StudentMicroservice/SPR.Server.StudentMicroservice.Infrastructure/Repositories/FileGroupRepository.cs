using SPR.Server.GroupMicroservice.Domain.Models;
using SPR.Server.StudentMicroservice.Domain.Interfaces;

namespace SPR.Server.StudentMicroservice.Infrastructure.Repositories
{
    public class FileGroupRepository : IGroupRepository
    {
        private List<Group> _groups;
        private readonly string _filePath;
        private readonly FileWorker.FileWorker _fileWorker;

        public FileGroupRepository(string filePath, FileWorker.FileWorker fileWorker)
        {
            _filePath = filePath;
            _fileWorker = fileWorker;
            try
            {
                _groups = _fileWorker.Read<List<Group>>(_filePath);
            }
            catch (ArgumentNullException ex)
            {
                _groups = new List<Group>();
            }
        }

        public void Add(Group group)
        {
            if (!_groups.Any(x => x.Id == group.Id))
            {
                _groups.Add(group);
                Save();
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
            _fileWorker.Write(_filePath, _groups);
        }

        public void Delete(Guid id)
        {
            _groups.RemoveAll(x => x.Id == id);
            Save();
        }

        public async Task DeleteAsync(Guid id)
        {
            Delete(id);
        }
    }
}
