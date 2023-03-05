using SPR.FileWorker;
using SPR.Server.AuthMicroservice.Domain.Interfaces;
using SPR.Server.AuthMicroservice.Domain.Models;
using SPR.Server.AuthMicroservice.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var filePath = builder.Configuration["UsersFilePath"];
if (filePath is null)
{
    throw new Exception("Not found configuration for file path");
}

var jwtTokenConfiguration = builder.Configuration.GetSection("JwtTokenConfiguration").Get<JwtTokenConfiguration>();
if (jwtTokenConfiguration is null)
{
    throw new ArgumentNullException(nameof(jwtTokenConfiguration));
}

builder.Services.AddSingleton<JwtTokenConfiguration>(jwtTokenConfiguration);
builder.Services.AddSingleton<FileWorker>();
builder.Services.AddSingleton<IUserRepository>(sp => new UserFileRepository(filePath, sp.GetRequiredService<FileWorker>()));
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