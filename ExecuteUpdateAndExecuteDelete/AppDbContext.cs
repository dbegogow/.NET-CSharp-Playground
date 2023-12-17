namespace ExecuteUpdateAndExcecuteDelete;

using ExecuteUpdateAndExcecuteDelete.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Company> Companies { get; init; }

    public DbSet<Employee> Employees { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Company>()
            .HasMany(c => c.Employees)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(builder);
    }
}
