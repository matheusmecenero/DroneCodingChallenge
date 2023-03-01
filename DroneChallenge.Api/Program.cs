using DroneChallenge.Service;
using DroneChallenge.Service.Interfaces;
using DroneChallenge.Application.UseCases;
using DroneChallenge.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddScoped<IDroneUseCase, DroneUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();