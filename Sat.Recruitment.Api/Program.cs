using System.Runtime.CompilerServices;
using System.Dynamic;
using System.Collections.Immutable;
using Sat.Recruitment.Business;
using Sat.Recruitment.Interfaces;
using Sat.Recruitment.Manager;
using Sat.Recruitment.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("appsettings." + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") + ".json",
    optional: true,
    reloadOnChange: true);
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

/* builder.Services.AddSingleton(typeof(IResult<>), typeof(Result<>));
builder.Services.AddSingleton(typeof(IResult<>), typeof(ResultError<>));
 */
//builder.Services.AddTransient<IResultMessage, ResultMessage>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserManager, UserManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
