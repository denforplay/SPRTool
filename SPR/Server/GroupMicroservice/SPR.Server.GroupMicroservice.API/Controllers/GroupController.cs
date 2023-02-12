using Microsoft.AspNetCore.Mvc;
using SPR.Server.GroupMicroservice.Domain.Interfaces;
using SPR.Server.GroupMicroservice.Domain.Models;
using SPR.Shared.Models.Group;

namespace SPR.Server.GroupMicroservice.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(GroupModel groupModel)
        {
            await _groupRepository.AddAsync(new Group
            {
                Id = Guid.NewGuid(),
                Name = groupModel.Name
            });

            return Ok();
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
    }
}