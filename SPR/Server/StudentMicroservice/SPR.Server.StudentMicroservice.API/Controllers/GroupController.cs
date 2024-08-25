using Microsoft.AspNetCore.Mvc;
using SPR.Server.GroupMicroservice.Domain.Models;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Shared.Models.Group;

namespace SPR.Server.StudentMicroservice.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public GroupController(IGroupRepository groupRepository, IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public async Task<GroupModel> AddGroup(CreateGroupModel groupModel)
        {
            var newGroup = new Group
            {
                Id = Guid.NewGuid(),
                Name = groupModel.Name
            };

            await _groupRepository.AddAsync(newGroup);

            return new GroupModel()
            {
                Id = newGroup.Id,
                Name = newGroup.Name
            };
        }

        [HttpGet]
        public async Task<GroupModel> GetGroupById(Guid id)
        {
            var group = await _groupRepository.ReadFirstByConditionAsync(group => group.Id == id);
            if (group is null)
            {
                return null;
            }

            return new GroupModel()
            {
                Id = group.Id,
                Name = group.Name,
            };
        }

        [HttpGet]
        public async Task<GroupModel> GetGroupByName(string name)
        {
            var group = await _groupRepository.ReadFirstByConditionAsync(group => group.Name == name);
            if (group is null)
            {
                return null;
            }

            return new GroupModel()
            {
                Id = group.Id,
                Name = group.Name,
            };
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<GroupModel>> GetAllGroups()
        {
            var groups = await _groupRepository.ReadAllAsync();
            var outputGroups = new List<GroupModel>();

            foreach(var group in groups)
            {
                outputGroups.Add(new GroupModel
                {
                    Id = group.Id,
                    Name = group.Name
                });
            }

            return outputGroups;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupById(Guid id)
        {
            await _groupRepository.DeleteAsync(id);
            await _studentRepository.DeleteByConditionAsync(x => x.GroupId == id);
            return Ok();
        }
    }
}