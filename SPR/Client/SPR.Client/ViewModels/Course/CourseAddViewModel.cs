using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Course;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace SPR.Client.ViewModels.Course
{
    public class CourseAddViewModel : ViewModelBase
    {
        public event Action<CourseModel> OnCourseAdded;

        private readonly CommandBase _moveFromSelectedToAvailableCommand;
        private readonly CommandBase _moveFromAvailableToSelectedCommand;
        private readonly CommandBase _addCourseCommand;

        private readonly CourseGroupAvailableTableViewModel _courseGroupAvailableTableViewModel;
        private readonly CourseChoosedGroupsTableViewModel _courseGroupChoosedGroupsTableViewModel;

        private readonly IGroupHttpService _groupHttpService;
        private readonly ICourseHttpService _courseHttpService;

        private string _courseName;

        public CourseAddViewModel(ICourseHttpService courseHttpService, IGroupHttpService groupHttpService)
        {
            _groupHttpService = groupHttpService;
            _courseHttpService = courseHttpService;
            _courseGroupAvailableTableViewModel = new CourseGroupAvailableTableViewModel(groupHttpService);
            _courseGroupChoosedGroupsTableViewModel = new CourseChoosedGroupsTableViewModel();
            _moveFromAvailableToSelectedCommand = new ActionCommand(MoveFromAvailableToSelected);
            _moveFromSelectedToAvailableCommand = new ActionCommand(MoveFromSelectedToAvailable);
            _addCourseCommand = new ActionCommand(() => Application.Current.Dispatcher.Invoke(async () => await AddCourse()));
        }

        public CommandBase AddCourseCommand => _addCourseCommand;
        public CommandBase MoveFromSelectedToAvailableCommand => _moveFromSelectedToAvailableCommand;
        public CommandBase MoveFromAvailableToSelectedCommand => _moveFromAvailableToSelectedCommand;
        public CourseGroupAvailableTableViewModel CourseGroupAvailableTableViewModel => _courseGroupAvailableTableViewModel;
        public CourseChoosedGroupsTableViewModel CourseGroupChoosedGroupsTableViewModel => _courseGroupChoosedGroupsTableViewModel;

        public string CourseName
        {
            get => _courseName;
            set
            {
                _courseName = value;
                OnPropertyChanged(CourseName);
            }
        }
        
        private void MoveFromSelectedToAvailable()
        {
            if (CourseGroupChoosedGroupsTableViewModel.SelectedGroup is not null)
            {
                var selectedGroup = CourseGroupChoosedGroupsTableViewModel.SelectedGroup;
                CourseGroupChoosedGroupsTableViewModel.Remove(selectedGroup);
                CourseGroupAvailableTableViewModel.Add(selectedGroup);
            }
        }

        private void MoveFromAvailableToSelected()
        {
            if (CourseGroupAvailableTableViewModel.SelectedGroup is not null)
            {
                var selectedGroup = CourseGroupAvailableTableViewModel.SelectedGroup;
                CourseGroupAvailableTableViewModel.Remove(selectedGroup);
                CourseGroupChoosedGroupsTableViewModel.Add(selectedGroup);
            }
        }

        private async Task AddCourse()
        {
            var createdCourseModel = new CreateCourseModel
            {
                Name = CourseName,
                Groups = CourseGroupChoosedGroupsTableViewModel.ChoosedGroups,
            };

            var model = await _courseHttpService.AddCourse(createdCourseModel);
            OnCourseAdded?.Invoke(model);
            CourseName = string.Empty;
            CourseGroupChoosedGroupsTableViewModel.Reload();
            CourseGroupAvailableTableViewModel.Reload();
        }
    }
}
