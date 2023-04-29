using WebApiWithMongoDb.Configurations;
using WebApiWithMongoDb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("MongoDatabase"));

builder.Services.AddTransient<IDriverService, DriverService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
