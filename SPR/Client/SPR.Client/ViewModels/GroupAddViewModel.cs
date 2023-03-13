using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Group;
using System;
using System.Windows.Input;

namespace SPR.Client.ViewModels
{
    public class GroupAddViewModel : ViewModelBase
    {
        public event Action<GroupModel> OnGroupAdded;
        private readonly IGroupHttpService _groupHttpService;
        private string _groupName;

        public CommandBase AddGroupCommand { get; }

        public GroupAddViewModel(IGroupHttpService groupHttpService)
        {
            _groupHttpService = groupHttpService;
            AddGroupCommand = new ActionCommand(AddGroupAction, CanAddGroup);
        }

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                OnPropertyChanged(GroupName);
                AddGroupCommand.RaiseExecuteChanged();
            }
        }

        private async void AddGroupAction()
        {
            if (!String.IsNullOrEmpty(GroupName))
            {
                var newGroup = new CreateGroupModel
                {
                    Name = GroupName
                };

                var createdGroup = await _groupHttpService.AddGroup(newGroup);
                GroupName = String.Empty;
                OnGroupAdded?.Invoke(createdGroup);
            }
        }

        private bool CanAddGroup()
        {
            return !string.IsNullOrEmpty(GroupName);
        }
    }
}
