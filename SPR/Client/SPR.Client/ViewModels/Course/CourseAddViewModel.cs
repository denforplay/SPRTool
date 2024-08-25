using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Course;
using SPR.Shared.Models.Group;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace SPR.Client.ViewModels.Course
{
    public class CourseAddViewModel : ViewModelBase
    {
        private bool _canUpdate;
        public event Action<CourseModel> OnCourseAdded;

        private readonly CommandBase _moveFromSelectedToAvailableCommand;
        private readonly CommandBase _moveFromAvailableToSelectedCommand;
        private readonly CommandBase _addCourseCommand;
        private readonly CommandBase _updateCourseCommand;
        private readonly CommandBase _addTaskCommand;

        private readonly CourseGroupAvailableTableViewModel _courseGroupAvailableTableViewModel;
        private readonly CourseChoosedGroupsTableViewModel _courseGroupChoosedGroupsTableViewModel;
        private readonly TaskTableViewModel _taskTableViewModel;

        private readonly IGroupHttpService _groupHttpService;
        private readonly ICourseHttpService _courseHttpService;

        private string _courseName;
        private string _taskName;
        private Guid? _groupId = null;

        public CourseAddViewModel(ICourseHttpService courseHttpService, IGroupHttpService groupHttpService)
        {
            _groupHttpService = groupHttpService;
            _courseHttpService = courseHttpService;
            _courseGroupAvailableTableViewModel = new CourseGroupAvailableTableViewModel(groupHttpService);
            _courseGroupChoosedGroupsTableViewModel = new CourseChoosedGroupsTableViewModel();
            _taskTableViewModel = new TaskTableViewModel();
            _moveFromAvailableToSelectedCommand = new ActionCommand(MoveFromAvailableToSelected);
            _moveFromSelectedToAvailableCommand = new ActionCommand(MoveFromSelectedToAvailable);
            _addCourseCommand = new ActionCommand(() => Application.Current.Dispatcher.Invoke(async () => await AddCourse()));
            _updateCourseCommand = new ActionCommand(() => Application.Current.Dispatcher.Invoke(async () => await UpdateCourse()));
            _addTaskCommand = new ActionCommand(AddTask, CanAddTask);
            CanUpdate = false;
        }

        public CommandBase AddTaskCommand => _addTaskCommand;
        public CommandBase AddCourseCommand => _addCourseCommand;
        public CommandBase UpdateCourseCommand => _updateCourseCommand;
        public CommandBase MoveFromSelectedToAvailableCommand => _moveFromSelectedToAvailableCommand;
        public CommandBase MoveFromAvailableToSelectedCommand => _moveFromAvailableToSelectedCommand;
        public CourseGroupAvailableTableViewModel CourseGroupAvailableTableViewModel => _courseGroupAvailableTableViewModel;
        public CourseChoosedGroupsTableViewModel CourseGroupChoosedGroupsTableViewModel => _courseGroupChoosedGroupsTableViewModel;
        public TaskTableViewModel TaskTableViewModel => _taskTableViewModel;

        public bool CanUpdate
        {
            get => _canUpdate;
            set
            {
                _canUpdate = value;
                OnPropertyChanged(nameof(CanUpdate));
            }
        }

        public string CourseName
        {
            get => _courseName;
            set
            {
                _courseName = value;
                OnPropertyChanged(CourseName);
            }
        }

        public string TaskName
        {
            get => _taskName;
            set
            {
                _taskName = value;
                OnPropertyChanged(TaskName);
                AddTaskCommand.RaiseExecuteChanged();
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

        public void SetCourseModel(CourseModel courseModel)
        {
            if (courseModel is null)
            {
                return;
            }

            _groupId = courseModel.Id;
            this.CourseName = courseModel.Name;
            this.TaskName = string.Empty;
            this.TaskTableViewModel.Tasks.Clear();
            this.CourseGroupChoosedGroupsTableViewModel.ChoosedGroups.Clear();

            foreach (var task in courseModel.Tasks)
            {
                this.TaskTableViewModel.Tasks.Add(new CreateTaskModel
                {
                    Name = task.Name
                });
            }

            foreach (var group in courseModel.Groups)
            {
                this.CourseGroupChoosedGroupsTableViewModel.Add(group);
            }

            CourseGroupAvailableTableViewModel.Reload(this.CourseGroupChoosedGroupsTableViewModel.ChoosedGroups.ToList());
            CanUpdate = true;
        }

        private async Task AddCourse()
        {
            var createdCourseModel = new CreateCourseModel
            {
                Name = CourseName,
                Groups = CourseGroupChoosedGroupsTableViewModel.ChoosedGroups,
                Tasks = TaskTableViewModel.Tasks
            };

            var model = await _courseHttpService.AddCourse(createdCourseModel);
            OnCourseAdded?.Invoke(model);
            CourseName = string.Empty;
            TaskTableViewModel.Reload();
            CourseGroupChoosedGroupsTableViewModel.Reload();
            CourseGroupAvailableTableViewModel.Reload();
            CanUpdate = false;
            _groupId = null;
        }

        private async Task UpdateCourse()
        {
            var updateCourseModel = new UpdateCourseModel
            {
                Id = _groupId!.Value,
                Name = CourseName,
                Groups = CourseGroupChoosedGroupsTableViewModel.ChoosedGroups,
                Tasks = TaskTableViewModel.Tasks
            };

            var model = await _courseHttpService.UpdateCourse(updateCourseModel);
        }

        private void AddTask()
        {
            var task = new CreateTaskModel { Name = _taskName };
            _taskTableViewModel.Tasks.Add(task);
            TaskName = string.Empty;
        }

        private bool CanAddTask()
        {
            return !string.IsNullOrEmpty(TaskName);
        }
    }
}
