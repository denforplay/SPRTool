using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Course;
using SPR.Shared.Models.Group;
using SPR.Shared.Models.Student;
using SPR.Shared.Models.StudentTask;
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
        private readonly IStudentTaskHttpService _studentTaskHttpService;
        private readonly IStudentHttpService _studentHttpService;
        private readonly ICourseHttpService _courseHttpService;
        private readonly ManagementCourseTableViewModel _managementCourseTableViewModel;
        private CourseModel _currentCourseModel;

        public ReoGridControl ReoGrid { get; set; }
        
        public ManagementViewModel(IStudentHttpService studentHttpService, ICourseHttpService courseHttpService, IStudentTaskHttpService studentTaskHttpService)
        {
            _studentTaskHttpService = studentTaskHttpService;
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
                ReoGrid.CurrentWorksheet.Cells[i * 2 + 1, 0].IsReadOnly = true;
                ReoGrid.CurrentWorksheet[i * 2 + 1, 0] = $"{students[i].Name} {students[i].Surname}";
            }

            for (int i = 0; i < _currentCourseModel.Tasks.Count; i++)
            {
                var task = _currentCourseModel.Tasks.ElementAt(i);
                ReoGrid.CurrentWorksheet.Cells[0, i + 1].IsReadOnly = true;
                ReoGrid.CurrentWorksheet[0, i + 1] = $"{task.Name}";

                for (int j = 0; j < students.Count; j++)
                {
                    var newJ = j;
                    CheckBoxCell checkbox;
                    var info = await _studentTaskHttpService.GetInfoAboutStudentTask(students[j].Id, task.Id);
                    if (info.IsCompleted)
                    {
                        checkbox = new CheckBoxCell(true);
                        ReoGrid.CurrentWorksheet[j * 2 + 1, i + 1] = info.CompletedTime;
                    }
                    else
                    {
                        checkbox = new CheckBoxCell(false);
                    }
                    var rowIndex = j * 2 + 1;
                    var colIndex = i + 1;

                    checkbox.CheckChanged += (_, _) => OnCheckChanged(students[newJ], task, checkbox.IsChecked, rowIndex, colIndex);
                    ReoGrid.CurrentWorksheet[j * 2 + 1, i + 1] = new object[] { checkbox };
                }
            }
        }

        private void OnCheckChanged(StudentModel studentModel, TaskModel taskModel, bool isCompleted, int rowIndex, int colIndex)
        {
            var updateStudentTask = new CreateStudentTaskModel
            {
                StudentId = studentModel.Id,
                TaskId = taskModel.Id,
                IsCompleted = isCompleted,
                CompletedTime = isCompleted ? DateTime.Now : null
            };

            if (updateStudentTask.IsCompleted)
            {
                ReoGrid.CurrentWorksheet[rowIndex + 1, colIndex] = updateStudentTask.CompletedTime.ToString();
            }
            else
            {
                ReoGrid.CurrentWorksheet[rowIndex + 1, colIndex] = "";
            }

            Application.Current.Dispatcher.Invoke(async () =>
            {
                var updatedTask = await _studentTaskHttpService.UpdateStudentTask(updateStudentTask);
            });
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