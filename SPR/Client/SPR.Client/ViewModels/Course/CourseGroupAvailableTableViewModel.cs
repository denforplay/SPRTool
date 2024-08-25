using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Group;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SPR.Client.ViewModels.Course
{
    public class CourseGroupAvailableTableViewModel : ViewModelBase
    {
        private readonly IGroupHttpService _groupHttpService;
        private ObservableCollection<GroupModel> _availableGroups;
        private GroupModel _selectedGroup;

        public CourseGroupAvailableTableViewModel(IGroupHttpService groupHttpService)
        {
            _groupHttpService = groupHttpService;
            Task.Run(async () => await LoadGroups());
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

        public ObservableCollection<GroupModel> AvailableGroups
        {
            get => _availableGroups;
            set
            {
                _availableGroups = value;
                OnPropertyChanged(nameof(AvailableGroups));
            }
        }

        public void Add(GroupModel groupModel)
        {
            _availableGroups.Add(groupModel);
        }

        public void Remove(GroupModel groupModel)
        {
            _availableGroups.Remove(groupModel);
        }

        public void Reload(List<GroupModel> choosedGroups = default)
        {
            Task.Run(async () => await LoadGroups(choosedGroups));
        }

        private async Task LoadGroups(List<GroupModel> choosedGroups = default)
        {
            if (choosedGroups == default)
            {
                var loadedGroups = await _groupHttpService.GetAllGroups();
                AvailableGroups = new ObservableCollection<GroupModel>(loadedGroups);
                return;
            }
            else
            {
                var loadedGroups = await _groupHttpService.GetAllGroups();
                AvailableGroups = new ObservableCollection<GroupModel>(loadedGroups.Where(x => !choosedGroups.Any(y => x.Name == y.Name)));
            }
        }
    }
}