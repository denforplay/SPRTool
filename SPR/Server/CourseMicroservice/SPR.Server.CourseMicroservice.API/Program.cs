using SPR.FileWorker;
using SPR.Server.CourseMicroservice.API.Services;
using SPR.Server.CourseMicroservice.Domain.Interfaces;
using SPR.Server.CourseMicroservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var filePath = builder.Configuration["CourseFilePath"];
var groupMicroserviceAddress = builder.Configuration["GroupMicroserviceAddress"];
if (filePath is null)
{
    throw new Exception("Not found configuration for file path");
}

builder.Services.AddSingleton<FileWorker>();
builder.Services.AddHttpClient<IGroupHttpService, GroupHttpService>(config =>
{
    config.BaseAddress = new Uri(groupMicroserviceAddress);
});
builder.Services.AddSingleton<ICourseRepository, CourseFileRepository>(sp => new CourseFileRepository(filePath, sp.GetRequiredService<FileWorker>()));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();