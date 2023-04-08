using InMemoryCachingWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemoryCachingWebApi.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; }
}
