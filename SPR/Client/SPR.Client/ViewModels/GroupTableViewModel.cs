using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Group;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SPR.Client.ViewModels
{
    public class GroupTableViewModel : ViewModelBase
    {
        private readonly IGroupHttpService _groupHttpService;
        private ObservableCollection<GroupModel> _groups;
        private GroupModel _selectedGroup;
        public ICommand DeleteCommand { get; }

        public GroupTableViewModel(IGroupHttpService groupHttpService)
        {
            _groupHttpService = groupHttpService;
            LoadGroups();
            DeleteCommand = new ActionCommand(DeleteAsync);
        }

        public GroupModel SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        public ObservableCollection<GroupModel> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        private async void DeleteAsync()
        {
            if (SelectedGroup is not null)
            {
                await _groupHttpService.DeleteGroup(SelectedGroup.Id);
                _groups.Remove(SelectedGroup);
            }
        }

        private async Task LoadGroups()
        {
            var loadedGroups = await _groupHttpService.GetAllGroups();
            Groups = new ObservableCollection<GroupModel>(loadedGroups);
        }
    }
}
