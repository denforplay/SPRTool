using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Course;
using SPR.Shared.Models.Group;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using unvell.ReoGrid;
using unvell.ReoGrid.CellTypes;
using unvell.ReoGrid.Events;

namespace SPR.Client.ViewModels.Management
{
    public class ManagementViewModel : ViewModelBase
    {
        private readonly IStudentHttpService _studentHttpService;
        private readonly ICourseHttpService _courseHttpService;
        private readonly ManagementCourseTableViewModel _managementCourseTableViewModel;
        private CourseModel _currentCourseModel;

        public ReoGridControl ReoGrid { get; set; }
        
        public ManagementViewModel(IStudentHttpService studentHttpService, ICourseHttpService courseHttpService)
        {
            _studentHttpService = studentHttpService;
            _courseHttpService = courseHttpService;
            _managementCourseTableViewModel = new ManagementCourseTableViewModel(courseHttpService);
            _managementCourseTableViewModel.OnCourseChanged += OnCourseChanged;
            ReoGrid = new ReoGridControl();
            ReoGrid.CurrentWorksheetChanged += OnWorksheetChanged;
        }

        public ManagementCourseTableViewModel ManagementCourseTableViewModel => _managementCourseTableViewModel;

        private void OnWorksheetChanged(object c, EventArgs args)
        {
            var groupModel = ManagementCourseTableViewModel.SelectedCourse.Groups.First(x => x.Name == ReoGrid.CurrentWorksheet.Name);
            ReoGrid.CurrentWorksheet.EnableSettings(WorksheetSettings.Edit_AutoExpandColumnWidth);
            Application.Current.Dispatcher.Invoke(async () => await GetStudentsFromGroup(groupModel));
        }

        private async Task GetStudentsFromGroup(GroupModel group)
        {
            var students = (await _studentHttpService.GetAllStudentsFromGroup(group.Id)).ToList();

            for (int i = 0; i < students.Count; i++)
            {
                ReoGrid.CurrentWorksheet.Cells[i + 1, 0].IsReadOnly = true;
                ReoGrid.CurrentWorksheet[i+1, 0] = $"{students[i].Name} {students[i].Surname}";
            }

            for (int i = 0; i < _currentCourseModel.Tasks.Count; i++)
            {
                ReoGrid.CurrentWorksheet.Cells[0, i + 1].IsReadOnly = true;
                ReoGrid.CurrentWorksheet[0, i + 1] = $"{_currentCourseModel.Tasks.ElementAt(i).Name}";

                for (int j = 0; j < students.Count; j++)
                {
                    var checkbox = new CheckBoxCell();
                    ReoGrid.CurrentWorksheet[j + 1, i + 1] = new object[] { checkbox };
                }
            }
        }

        private void OnCourseChanged(CourseModel courseModel)
        {
            _currentCourseModel = courseModel;
            ReoGrid.Worksheets.Clear();

            foreach(var group in courseModel.Groups)
            {
                var worksheet = ReoGrid.CreateWorksheet(group.Name);
                ReoGrid.AddWorksheet(worksheet);
            }
        }
    }
}