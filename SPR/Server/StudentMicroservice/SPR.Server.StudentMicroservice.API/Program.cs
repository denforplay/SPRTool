using SPR.FileWorker;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Server.StudentMicroservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var studentFilePath = builder.Configuration["StudentFilePath"];
var groupFilePath = builder.Configuration["GroupFilePath"];
var courseFilePath = builder.Configuration["CourseFilePath"];
var studentsTasksFilePath = builder.Configuration["StudentsTasksFilePath"];
builder.Services.AddSingleton<FileWorker>();
builder.Services.AddSingleton<IStudentRepository>(sp => new FileStudentRepository(studentFilePath, sp.GetRequiredService<FileWorker>()));
builder.Services.AddSingleton<IGroupRepository>(sp => new FileGroupRepository(groupFilePath, sp.GetRequiredService<FileWorker>()));
builder.Services.AddSingleton<ICourseRepository>(sp => new CourseFileRepository(courseFilePath, sp.GetRequiredService<FileWorker>()));
builder.Services.AddSingleton<IStudentsTasksRepository>(sp => new StudentsTasksFileRepository(studentsTasksFilePath, sp.GetRequiredService<FileWorker>()));

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