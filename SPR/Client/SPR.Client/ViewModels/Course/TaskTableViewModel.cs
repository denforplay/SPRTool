using SPR.Client.Abstractions.Core;
using SPR.Shared.Models.Course;
using SPR.Shared.Models.Group;
using System.Collections.ObjectModel;

namespace SPR.Client.ViewModels.Course
{
    public class TaskTableViewModel : ViewModelBase
    {
        private ObservableCollection<CreateTaskModel> _tasks;

        public TaskTableViewModel()
        {
            _tasks = new ObservableCollection<CreateTaskModel>();
        }

        public ObservableCollection<CreateTaskModel> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public void Reload()
        {
            _tasks.Clear();
        }
    }
}
