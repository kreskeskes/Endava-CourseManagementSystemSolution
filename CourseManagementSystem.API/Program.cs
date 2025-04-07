using CourseManagementSystem.API.Middlewares;
using CourseManagementSystem.Core;
using CourseManagementSystem.Infrastructure;
using CourseManagementSystem.Infrastructure.SeedIdentity;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedIdentity.SeedRolesAsync(services);
}


app.UseSwagger();
app.UseSwaggerUI();

app.Run();
