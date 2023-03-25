using CachingWebApiWithRedis.Models;
using Microsoft.EntityFrameworkCore;

namespace CachingWebApiWithRedis.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; }
}
