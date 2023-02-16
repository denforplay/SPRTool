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

        public ICommand AddGroupCommand { get; }

        public GroupAddViewModel(IGroupHttpService groupHttpService)
        {
            _groupHttpService = groupHttpService;
            AddGroupCommand = new ActionCommand(AddGroupAction);
        }

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                OnPropertyChanged(_groupName);
            }
        }

        private async void AddGroupAction()
        {
            if (!String.IsNullOrEmpty(GroupName))
            {
                var newGroup = new GroupModel
                {
                    Name = GroupName
                };

                await _groupHttpService.AddGroup(newGroup);
                GroupName = String.Empty;
                OnGroupAdded?.Invoke(newGroup);
            }
        }
    }
}
