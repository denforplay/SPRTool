using SPR.Client.Abstractions.Http;
using SPR.Shared.Models.Course;
using System.Net.Http.Json;

namespace SPR.Client.Communication.Http
{
    public class CourseHttpService : ICourseHttpService
    {
        private readonly HttpClient _courseClient;

        public CourseHttpService(HttpClient courseClient)
        {
            _courseClient = courseClient;
        }

        public async Task<CourseModel> AddCourse(CreateCourseModel courseModel)
        {
            var result = await _courseClient.PostAsJsonAsync<CreateCourseModel>($"/Course/AddCourse", courseModel);
            var createdModel = await result.Content.ReadFromJsonAsync<CourseModel>();
            return createdModel;
        }

        public async Task<CourseModel> UpdateCourse(UpdateCourseModel courseModel)
        {
            var result = await _courseClient.PutAsJsonAsync<UpdateCourseModel>($"/Course/UpdateCourse", courseModel);
            var createdModel = await result.Content.ReadFromJsonAsync<CourseModel>();
            return createdModel;
        }

        public async Task DeleteCourse(Guid id)
        {
            await _courseClient.DeleteAsync($"/Course/DeleteCourseById?id={id}");
        }

        public async Task<IReadOnlyCollection<CourseModel>> GetAllCourses()
        {
            var groups = await _courseClient.GetFromJsonAsync<IReadOnlyCollection<CourseModel>>($"/Course/GetAllCourses");
            return groups;
        }
    }
}