using Microsoft.EntityFrameworkCore;
using VF.Application.Features.Vehicles.Applications;
using VF.Application.Features.Vehicles.Applications.Src;
using VF.Database.Context;
using VF.Database.Features.Vehicles.Repositories;
using VF.Domain.Features.Vehicles.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VehicleFleetDbContext>(options =>
    options.UseInMemoryDatabase("MemoryDbVehicleFleetDbContext"));

builder.Services.AddAutoMapper(typeof(VehicleApplication).Assembly);
builder.Services.AddAutoMapper(typeof(VehicleRepository).Assembly);

builder.Services.AddScoped<IVehicleApplication, VehicleApplication>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("Default");

app.MapControllers();


app.Run();
