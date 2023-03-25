using SPR.FileWorker;
using SPR.Server.StudentsTasksMicroservice.Domain.Interfaces;
using SPR.Server.StudentsTasksMicroservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


var filePath = builder.Configuration["StudentTasksFilePath"];
builder.Services.AddSingleton<FileWorker>();
builder.Services.AddSingleton<IStudentsTasksRepository>(sp => new StudentsTasksFileRepository(filePath, sp.GetRequiredService<FileWorker>()));
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
