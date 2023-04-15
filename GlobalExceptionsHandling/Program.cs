using GlobalExceptionsHandling.Configurations;
using GlobalExceptionsHandling.Data;
using GlobalExceptionsHandling.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<ApiDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("SampleDbConnection")));

builder.Services
    .AddScoped<IDriverService, DriverService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AddGlobalErrorHandler();

app.Run();
