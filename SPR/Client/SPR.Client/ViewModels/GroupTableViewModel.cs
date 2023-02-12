using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Group;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SPR.Client.ViewModels
{
    public class GroupTableViewModel : ViewModelBase
    {
        private readonly IGroupHttpService _groupHttpService;
        private ObservableCollection<GroupModel> _groups;

        public GroupTableViewModel(IGroupHttpService groupHttpService)
        {
            _groupHttpService = groupHttpService;
            LoadGroups();
        }

        private async Task LoadGroups()
        {
            var loadedGroups = await _groupHttpService.GetAllGroups();
            Groups = new ObservableCollection<GroupModel>(loadedGroups);
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
    }
}
