using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Commands;
using SPR.Shared.Models.Course;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SPR.Client.ViewModels.Course
{
    public class CourseTableViewModel : ViewModelBase
    {
        public event Action<CourseModel> OnCourseChoosed;

        private readonly ICourseHttpService _courseHttpService;
        private ObservableCollection<CourseModel> _courses;
        private CourseModel _selectedCourse;
        public ActionCommand DeleteCommand { get; }

        public CourseTableViewModel(ICourseHttpService groupHttpService)
        {
            _courseHttpService = groupHttpService;
            _courses = new ObservableCollection<CourseModel>();
            Task.Run(async () => await LoadGroups());
            DeleteCommand = new ActionCommand(async () => await DeleteAsync());
        }

        public CourseModel SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged(nameof(SelectedCourse));
                DeleteCommand.RaiseExecuteChanged();
                OnCourseChoosed?.Invoke(SelectedCourse);
            }
        }

        public ObservableCollection<CourseModel> Courses
        {
            get => _courses;
            set
            {
                _courses = value;
                OnPropertyChanged(nameof(Courses));
            }
        }

        private async Task DeleteAsync()
        {
            await _courseHttpService.DeleteCourse(SelectedCourse.Id);
            _courses.Remove(SelectedCourse);
        }

        private async Task LoadGroups()
        {
            var loadedGroups = await _courseHttpService.GetAllCourses();
            Courses = new ObservableCollection<CourseModel>(loadedGroups);
        }
    }
}
