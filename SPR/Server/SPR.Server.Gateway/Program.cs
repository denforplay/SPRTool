using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("gatewayConfig.json");
builder.Services.AddOcelot();


var app = builder.Build();
app.UseHttpsRedirection();
app.UseOcelot().Wait();
app.Run();
