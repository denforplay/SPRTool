using SPR.FileWorker;
using SPR.Server.CourseMicroservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var filePath = builder.Configuration["CourseFilePath"];
if (filePath is null)
{
    throw new Exception("Not found configuration for file path");
}

builder.Services.AddSingleton<FileWorker>();
builder.Services.AddSingleton(sp => new CourseFileRepository(filePath, sp.GetRequiredService<FileWorker>()));
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