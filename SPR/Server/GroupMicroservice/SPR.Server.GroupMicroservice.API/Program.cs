using SPR.FileWorker;
using SPR.Server.GroupMicroservice.Domain.Interfaces;
using SPR.Server.GroupMicroservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var filePath = builder.Configuration["GroupFilePath"];
builder.Services.AddSingleton<FileWorker>();
builder.Services.AddSingleton<IGroupRepository>(sp => new FileGroupRepository(filePath, sp.GetRequiredService<FileWorker>()));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
