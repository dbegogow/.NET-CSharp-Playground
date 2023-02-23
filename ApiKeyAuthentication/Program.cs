using ApiKeyAuthentication.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "The API Key to access the API",
        Type = SecuritySchemeType.ApiKey,
        Name = "x-api-key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };

    var requirements = new OpenApiSecurityRequirement
    {
        {scheme, new List<string>() }
    };

    x.AddSecurityRequirement(requirements);
});
builder.Services.AddAuthorization();
builder.Services.AddControllers(/*x => x.Filters.Add<ApiKeyAuthFilter>()*/);

builder.Services.AddScoped<ApiKeyAuthFilter>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Map("hello3", () => "Hello 3")
    .AddEndpointFilter<ApiKeyEndpointFilter>();

app.Run();