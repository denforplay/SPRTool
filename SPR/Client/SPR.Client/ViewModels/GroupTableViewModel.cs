using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Group;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SPR.Client.ViewModels
{
    public class GroupTableViewModel : ViewModelBase
    {
        private readonly IGroupHttpService _groupHttpService;
        private ObservableCollection<GroupModel> _groups;
        private GroupModel _selectedGroup;
        public ActionCommand DeleteCommand { get; }

        public GroupTableViewModel(IGroupHttpService groupHttpService)
        {
            _groupHttpService = groupHttpService;
            LoadGroups();
            DeleteCommand = new ActionCommand(async () => await DeleteAsync());
        }

        public GroupModel SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
                DeleteCommand.RaiseExecuteChanged();
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

        private async Task DeleteAsync()
        {
            await _groupHttpService.DeleteGroup(SelectedGroup.Id);
            _groups.Remove(SelectedGroup);
        }

        private async Task LoadGroups()
        {
            var loadedGroups = await _groupHttpService.GetAllGroups();
            Groups = new ObservableCollection<GroupModel>(loadedGroups);
        }
    }
}
