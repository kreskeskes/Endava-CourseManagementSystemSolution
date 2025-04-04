using CourseManagementSystem.Core;
using CourseManagementSystem.Infrastructure;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
