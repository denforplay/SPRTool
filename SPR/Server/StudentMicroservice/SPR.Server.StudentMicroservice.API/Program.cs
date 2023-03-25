using SPR.FileWorker;
using SPR.Server.StudentMicroservice.API.Controllers;
using SPR.Server.StudentMicroservice.Domain.Interfaces;
using SPR.Server.StudentMicroservice.Infrastructure.Repositories;
using SPR.Server.StudentMicroservice.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var filePath = builder.Configuration["StudentFilePath"];
var groupMicroserviceAddress = builder.Configuration["GroupMicroserviceAddress"];
builder.Services.AddSingleton<FileWorker>();
builder.Services.AddSingleton<IStudentRepository>(sp => new FileStudentRepository(filePath, sp.GetRequiredService<FileWorker>()));
builder.Services.AddSingleton<IGroupHttpService, GroupHttpService>();
builder.Services.AddHttpClient<IGroupHttpService, GroupHttpService>(config =>
{
    config.BaseAddress = new Uri(groupMicroserviceAddress);
});

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
