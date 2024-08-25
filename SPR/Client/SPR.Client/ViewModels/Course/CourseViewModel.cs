using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Course;
using System.Linq;

namespace SPR.Client.ViewModels.Course
{
    public class CourseViewModel : ViewModelBase
    {
        private readonly CourseAddViewModel _courseAddViewModel;
        private readonly CourseTableViewModel _courseTableViewModel;
        
        private readonly IGroupHttpService _groupHttpService;
        private readonly ICourseHttpService _courseHttpService;

        public CourseViewModel(IGroupHttpService groupHttpService,  ICourseHttpService courseHttpService)
        {
            _groupHttpService = groupHttpService;
            _courseHttpService = courseHttpService;
            _courseTableViewModel = new CourseTableViewModel(courseHttpService);
            _courseAddViewModel = new CourseAddViewModel(courseHttpService, groupHttpService);
            _courseTableViewModel.OnCourseChoosed += OnCourseSelected;
            _courseAddViewModel.OnCourseAdded += AddCourse;
        }

        private void OnCourseSelected(CourseModel courseModel)
        {
            _courseAddViewModel.SetCourseModel(courseModel);
        }

        private void AddCourse(CourseModel courseModel)
        {
            CourseTableViewModel.Courses.Add(courseModel);
        }

        public CourseTableViewModel CourseTableViewModel => _courseTableViewModel;
        public CourseAddViewModel CourseAddViewModel => _courseAddViewModel;
    }
}