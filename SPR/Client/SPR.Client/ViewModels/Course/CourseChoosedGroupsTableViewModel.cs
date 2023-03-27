using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Group;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SPR.Client.ViewModels.Course
{
    public class CourseChoosedGroupsTableViewModel : ViewModelBase
    {
        private ObservableCollection<GroupModel> _choosedGroups;
        private GroupModel _selectedGroup;

        public CourseChoosedGroupsTableViewModel()
        {
            _choosedGroups = new ObservableCollection<GroupModel>();
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

        public ObservableCollection<GroupModel> ChoosedGroups
        {
            get => _choosedGroups;
            set
            {
                _choosedGroups = value;
                OnPropertyChanged(nameof(ChoosedGroups));
            }
        }

        public void Reload()
        {
            _choosedGroups.Clear();
        }

        public void Add(GroupModel groupModel)
        {
            _choosedGroups.Add(groupModel);
        }

        public void Remove(GroupModel groupModel)
        {
            _choosedGroups.Remove(groupModel);
        }
    }
}
