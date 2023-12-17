using ExecuteUpdateAndExcecuteDelete;
using ExecuteUpdateAndExcecuteDelete.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetValue<string>("DbConnectionString");

builder.Services
    .AddDbContext<AppDbContext>(options => options
        .UseSqlServer(dbConnectionString))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPut("/increase-salaries", async (int companyId, AppDbContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .Include(c => c.Employees)
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound(
            $"The company with Id '{companyId}' was not found.");
    }

    foreach (var employee in company.Employees)
    {
        employee.Salary += 100;
    }

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/increase-salaries-v2", async (int companyId, AppDbContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound(
            $"The company with Id '{companyId}' was not found.");
    }

    await dbContext.Set<Employee>()
        .Where(e => e.CompanyId == companyId)
        .ExecuteUpdateAsync(s => s.SetProperty(
            e => e.Salary,
            e => e.Salary + 100));

    return Results.NoContent();
});

app.MapDelete("/delete-employees", async (
    int companyId,
    decimal salaryThreshold,
    AppDbContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound(
            $"The company with Id '{companyId}' was not found.");
    }

    await dbContext.Set<Employee>()
        .Where(e => e.CompanyId == companyId
                    && e.Salary > salaryThreshold)
        .ExecuteDeleteAsync();

    return Results.NoContent();
});

app.Run();
