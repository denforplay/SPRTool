using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Course;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace SPR.Client.ViewModels.Management
{
    public class ManagementCourseTableViewModel : ViewModelBase
    {
        public event Action<CourseModel> OnCourseChanged;
        private readonly ICourseHttpService _courseHttpService;
        private CourseModel _selectedCourse;
        private ObservableCollection<CourseModel> _courses;

        public ManagementCourseTableViewModel(ICourseHttpService courseHttpService)
        {
            _courseHttpService = courseHttpService;
            _courses = new ObservableCollection<CourseModel>();
            Application.Current.Dispatcher.Invoke(async () => await GetAllCourses());
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

        public CourseModel SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged(nameof(SelectedCourse));
                OnCourseChanged?.Invoke(_selectedCourse);
            }
        }

        private async Task GetAllCourses()
        {
            var courses = await _courseHttpService.GetAllCourses();

            Courses = new ObservableCollection<CourseModel>(courses);
        }
    }
}