using BrokerAPI;
using BrokerAPI.Filters;
using clean.Application;
using clean.Infrastructure;
using clean.Infrastructure.Identity;
using clean.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(p => p.Filters.Add(new ApiExceptionFilterAttribute()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.RegisterAuthorization(builder.Configuration);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleService = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    await ApplicationDbContextSeed.SeedDefaultUserAsync(userService, roleService);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
