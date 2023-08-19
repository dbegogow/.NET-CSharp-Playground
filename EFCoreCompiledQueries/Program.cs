using System.Diagnostics;

using EFCoreCompiledQueries.Data;
using EFCoreCompiledQueries.Data.Models;

using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Attributes;

using Microsoft.EntityFrameworkCore;

namespace EFCoreCompiledQueries;

public class Program
{
    private static SoundWaveDbContext _context;

    private static void Main()
    {
        var builder = new DbContextOptionsBuilder<SoundWaveDbContext>();
        builder.UseSqlServer(
            "Server=.;Database=SoundWave;Trusted_Connection=True;TrustServerCertificate=True;");

        var dbContextOptions = builder.Options;
        _context = new SoundWaveDbContext(dbContextOptions);

        _context.Artists.First();

        RunTest(
            albumIDs =>
            {
                var l = new List<Album>();
                foreach (var id in albumIDs)
                {
                    l.Add(_context.Albums
                        .FirstOrDefault(a => a.Id == id));
                }
            },
            name: "Run-time EF Core Query");

        RunTest(
            albumIDs =>
            {
                var explicitQuery = EF.CompileQuery((SoundWaveDbContext context, int id)
                    => _context.Albums.FirstOrDefault(a => a.Id == id));

                var l = new List<Album>();
                foreach (var id in albumIDs)
                {
                    l.Add(explicitQuery(_context, id));
                }
            },
            name: "Compiled EF Core Query");

        RunTest(
            albumIDs =>
            {
                var compiledExplicitQuery = EF.CompileAsyncQuery((SoundWaveDbContext context, int id)
                    => context.Albums.FirstOrDefault(a => a.Id == id));

                var l = new List<Task>();
                foreach (var id in albumIDs)
                {
                    l.Add(compiledExplicitQuery(_context, id));
                }
                Task.WaitAny(l.ToArray());
            },
            name: "Async Compiled EF Core Query");

        RunTest(
            albumIDs =>
            {
                var l = new List<Album>();
                foreach (var id in albumIDs)
                {
                    l.Add(_context.GetAlbum(id));
                }
            },
            name: "DBContext Compiled EF Core Query");

        RunTest(
            albumIDs =>
            {
                var l = new List<Task>();
                foreach (var id in albumIDs)
                {
                    l.Add(_context.GetAlbumAsync(id));
                }
                Task.WaitAny(l.ToArray());
            },
            name: "DBContext Async Compiled EF Core Query");
    }

    private static void RunTest(Action<int[]> test, string name)
    {
        var albumIDs = GetAlbumIDs(500);
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        test(albumIDs);

        stopwatch.Stop();

        Console.WriteLine($"{name}:  {stopwatch.ElapsedMilliseconds,4}ms");
    }

    private static int[] GetAlbumIDs(int count)
    {
        var albums = Queryable.Take(_context!.Albums, count);

        return albums
                .AsEnumerable()
                .Select(i => i.Id)
                .ToArray();
    }
}

[SimpleJob(RuntimeMoniker.Net70)]
public class CmpldQryBenchmark
{
    private int[] _albumIDs;
    private static SoundWaveDbContext _context;

    [Params(500)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        var builder = new DbContextOptionsBuilder<SoundWaveDbContext>();
        builder.UseSqlServer(
            "Server=.;Database=SoundWave;Trusted_Connection=True;TrustServerCertificate=True;");

        var dbContextOptions = builder.Options;
        _context = new SoundWaveDbContext(dbContextOptions);

        // Warm up
        _context.Artists.First();

        _albumIDs = GetAlbumIDs(N);
    }

    private static int[] GetAlbumIDs(int count)
    {
        IQueryable<Album> albums = Queryable.Take(_context!.Albums, count);

        return albums.AsEnumerable().Select(i => i.Id).ToArray();
    }

    [Benchmark]
    public void RunTimeEFCoreQuery()
    {
        List<Album?> l = new List<Album?>();
        foreach (var id in _albumIDs)
        {
            // Use a regular auto-compiled query
            l.Add(_context.Albums.FirstOrDefault(a => a.Id == id));
        }
    }

    [Benchmark]
    public void CompiledEFCoreQuery()
    {
        // Create explicit compiled query
        var explicitQuery = EF.CompileQuery((SoundWaveDbContext context, int id)
            => _context.Albums.FirstOrDefault(a => a.Id == id));

        List<Album?> l = new List<Album?>();
        foreach (var id in _albumIDs)
        {
            // Invoke the compiled query
            l.Add(explicitQuery(_context, id));
        }
    }

    [Benchmark]
    public void AsyncCompiledEFCoreQuery()
    {
        // Create explicit compiled query
        var compiledExplicitQuery = EF.CompileAsyncQuery((SoundWaveDbContext context, int id)
            => context.Albums.FirstOrDefault(a => a.Id == id));

        List<Task> l = new List<Task>();
        foreach (var id in _albumIDs)
        {
            // Invoke the compiled async query
            l.Add(compiledExplicitQuery(_context, id));
        }
        Task.WaitAny(l.ToArray());
    }

    [Benchmark]
    public void DBContextCompiledEFCoreQuery()
    {
        List<Album?> l = new List<Album?>();
        foreach (var id in _albumIDs)
        {
            // Invoke the compiled query from DBContext
            l.Add(_context.GetAlbum(id));
        }
    }

    [Benchmark]
    public void DBContextAsyncCompiledEFCoreQuery()
    {
        List<Task> l = new List<Task>();
        foreach (var id in _albumIDs)
        {
            // Invoke the compiled async query from DBContext;
            l.Add(_context.GetAlbumAsync(id));
        }
        Task.WaitAny(l.ToArray());
    }
}