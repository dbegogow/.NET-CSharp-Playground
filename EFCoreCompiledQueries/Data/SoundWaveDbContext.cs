using EFCoreCompiledQueries.Data.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreCompiledQueries.Data;

public class SoundWaveDbContext : DbContext
{
    private static readonly Func<SoundWaveDbContext, int, bool> _queryAlbumExists =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Albums.Any(a => a.Id == id));

    private static readonly Func<SoundWaveDbContext, IEnumerable<Album>> _queryGetAllAlbums =
        EF.CompileQuery((SoundWaveDbContext db) => db.Albums);

    private static readonly Func<SoundWaveDbContext, int, Album> _queryGetAlbum =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Albums.FirstOrDefault(a => a.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Album>> _queryGetAlbumsByArtistId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Albums.Where(a => a.ArtistId == id));

    private static readonly Func<SoundWaveDbContext, IEnumerable<Artist>> _queryGetAllArtists =
        EF.CompileQuery((SoundWaveDbContext db) => db.Artists);

    private static readonly Func<SoundWaveDbContext, int, Artist> _queryGetArtist =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Artists.FirstOrDefault(a => a.Id == id));

    private static readonly Func<SoundWaveDbContext, IEnumerable<Customer>> _queryGetAllCustomers =
        EF.CompileQuery((SoundWaveDbContext db) => db.Customers);

    private static readonly Func<SoundWaveDbContext, int, Customer> _queryGetCustomer =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Customers.FirstOrDefault(c => c.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Customer>> _queryGetCustomerBySupportRepId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Customers.Where(a => a.SupportRepId == id));

    private static readonly Func<SoundWaveDbContext, IEnumerable<Employee>> _queryGetAllEmployees =
        EF.CompileQuery((SoundWaveDbContext db) => db.Employees);

    private static readonly Func<SoundWaveDbContext, int, Employee> _queryGetEmployee =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Employees.FirstOrDefault(e => e.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Employee>> _queryGetDirectReports =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Employees.Where(e => e.ReportsTo == id));

    private static readonly Func<SoundWaveDbContext, int, Employee> _queryGetReportsTo =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Employees.First(e => e.ReportsTo == id));

    private static readonly Func<SoundWaveDbContext, IEnumerable<Genre>> _queryGetAllGenres =
        EF.CompileQuery((SoundWaveDbContext db) => db.Genres);

    private static readonly Func<SoundWaveDbContext, int, Genre> _queryGetGenre =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Genres.FirstOrDefault(g => g.Id == id));

    private static readonly Func<SoundWaveDbContext, IEnumerable<InvoiceLine>> _queryGetAllInvoiceLines =
        EF.CompileQuery((SoundWaveDbContext db) => db.InvoiceLines);

    private static readonly Func<SoundWaveDbContext, int, InvoiceLine> _queryGetInvoiceLine =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.InvoiceLines.FirstOrDefault(i => i.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<InvoiceLine>> _queryGetInvoiceLinesByInvoiceId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.InvoiceLines.Where(a => a.InvoiceId == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<InvoiceLine>> _queryGetInvoiceLinesByTrackId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.InvoiceLines.Where(a => a.TrackId == id));

    private static readonly Func<SoundWaveDbContext, IEnumerable<Invoice>> _queryGetAllInvoices =
        EF.CompileQuery((SoundWaveDbContext db) => db.Invoices);

    private static readonly Func<SoundWaveDbContext, int, Invoice> _queryGetInvoice =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Invoices.FirstOrDefault(i => i.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Invoice>> _queryGetInvoicesByCustomerId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Invoices.Where(a => a.CustomerId == id));

    private static readonly Func<SoundWaveDbContext, IEnumerable<MediaType>> _queryGetAllMediaTypes =
        EF.CompileQuery((SoundWaveDbContext db) => db.MediaTypes);

    private static readonly Func<SoundWaveDbContext, int, MediaType> _queryGetMediaType =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.MediaTypes.FirstOrDefault(m => m.Id == id));

    private static readonly Func<SoundWaveDbContext, IEnumerable<Playlist>> _queryGetAllPlaylists =
        EF.CompileQuery((SoundWaveDbContext db) => db.Playlists);

    private static readonly Func<SoundWaveDbContext, int, Playlist> _queryGetPlaylist =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Playlists.FirstOrDefault(p => p.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Playlist>> _queryGetPlaylistsByTrackId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Tracks.Where(t => t.Id == id).SelectMany(t => t.Playlists!));

    private static readonly Func<SoundWaveDbContext, IEnumerable<Track>> _queryGetAllTracks =
        EF.CompileQuery((SoundWaveDbContext db) => db.Tracks);

    private static readonly Func<SoundWaveDbContext, int, Track> _queryGetTrack =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Tracks.FirstOrDefault(t => t.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Track>> _queryGetTracksByAlbumId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Tracks.Where(a => a.AlbumId == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Track>> _queryGetTracksByGenreId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Tracks.Where(a => a.GenreId == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Track>> _queryGetTracksByMediaTypeId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Tracks.Where(a => a.MediaTypeId == id));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Track>> _queryGetTracksByArtistId =
        EF.CompileQuery((SoundWaveDbContext db, int id) =>
            db.Albums.Where(a => a.ArtistId == id).SelectMany(t => t.Tracks));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Track>> _queryGetTracksByInvoiceId =
        EF.CompileQuery((SoundWaveDbContext db, int id) =>
            db.Tracks.Where(c => c.InvoiceLines.Any(o => o.InvoiceId == id)));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Invoice>> _queryGetInvoicesByEmployeeId =
        EF.CompileQuery((SoundWaveDbContext db, int id) =>
            db.Customers.Where(a => a.SupportRepId == id).SelectMany(t => t.Invoices));

    private static readonly Func<SoundWaveDbContext, int, IEnumerable<Track>> _queryGetTracksByPlaylistId =
        EF.CompileQuery((SoundWaveDbContext db, int id) => db.Playlists.Where(a => a.Id == id).SelectMany(t => t.Tracks!));

    private static readonly Func<SoundWaveDbContext, int, Task<bool>> _queryAlbumExistsAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Albums.Any(a => a.Id == id));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<Album>> _queryGetAllAlbumsAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.Albums);

    private static readonly Func<SoundWaveDbContext, int, Task<Album>> _queryGetAlbumAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Albums.FirstOrDefault(a => a.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Album>> _queryGetAlbumsByArtistIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Albums.Where(a => a.ArtistId == id));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<Artist>> _queryGetAllArtistsAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.Artists);

    private static readonly Func<SoundWaveDbContext, int, Task<Artist>> _queryGetArtistAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Artists.FirstOrDefault(a => a.Id == id));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<Customer>> _queryGetAllCustomersAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.Customers);

    private static readonly Func<SoundWaveDbContext, int, Task<Customer>> _queryGetCustomerAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Customers.FirstOrDefault(c => c.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Customer>> _queryGetCustomerBySupportRepIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Customers.Where(a => a.SupportRepId == id));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<Employee>> _queryGetAllEmployeesAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.Employees);

    private static readonly Func<SoundWaveDbContext, int, Task<Employee>> _queryGetEmployeeAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Employees.FirstOrDefault(e => e.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Employee>> _queryGetDirectReportsAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Employees.Where(e => e.ReportsTo == id));

    private static readonly Func<SoundWaveDbContext, int, Task<Employee>> _queryGetReportsToAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Employees.First(e => e.ReportsTo == id));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<Genre>> _queryGetAllGenresAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.Genres);

    private static readonly Func<SoundWaveDbContext, int, Task<Genre>> _queryGetGenreAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Genres.FirstOrDefault(g => g.Id == id));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<InvoiceLine>> _queryGetAllInvoiceLinesAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.InvoiceLines);

    private static readonly Func<SoundWaveDbContext, int, Task<InvoiceLine>> _queryGetInvoiceLineAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.InvoiceLines.FirstOrDefault(i => i.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<InvoiceLine>> _queryGetInvoiceLinesByInvoiceIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.InvoiceLines.Where(a => a.InvoiceId == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<InvoiceLine>> _queryGetInvoiceLinesByTrackIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.InvoiceLines.Where(a => a.TrackId == id));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<Invoice>> _queryGetAllInvoicesAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.Invoices);

    private static readonly Func<SoundWaveDbContext, int, Task<Invoice>> _queryGetInvoiceAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Invoices.FirstOrDefault(i => i.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Invoice>> _queryGetInvoicesByCustomerIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Invoices.Where(a => a.CustomerId == id));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<MediaType>> _queryGetAllMediaTypesAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.MediaTypes);

    private static readonly Func<SoundWaveDbContext, int, Task<MediaType>> _queryGetMediaTypeAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.MediaTypes.FirstOrDefault(m => m.Id == id));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<Playlist>> _queryGetAllPlaylistsAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.Playlists);

    private static readonly Func<SoundWaveDbContext, int, Task<Playlist>> _queryGetPlaylistAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Playlists.FirstOrDefault(p => p.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Playlist>> _queryGetPlaylistsByTrackIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Tracks.Where(t => t.Id == id).SelectMany(t => t.Playlists!));

    private static readonly Func<SoundWaveDbContext, IAsyncEnumerable<Track>> _queryGetAllTracksAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db) => db.Tracks);

    private static readonly Func<SoundWaveDbContext, int, Task<Track>> _queryGetTrackAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Tracks.FirstOrDefault(t => t.Id == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Track>> _queryGetTracksByAlbumIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Tracks.Where(a => a.AlbumId == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Track>> _queryGetTracksByGenreIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Tracks.Where(a => a.GenreId == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Track>> _queryGetTracksByMediaTypeIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Tracks.Where(a => a.MediaTypeId == id));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Track>> _queryGetTracksByArtistIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Albums.Where(a => a.ArtistId == id).SelectMany(t => t.Tracks));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Track>> _queryGetTracksByInvoiceIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Tracks.Where(c => c.InvoiceLines.Any(o => o.InvoiceId == id)));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Invoice>> _queryGetInvoicesByEmployeeIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Customers.Where(a => a.SupportRepId == id).SelectMany(t => t.Invoices));

    private static readonly Func<SoundWaveDbContext, int, IAsyncEnumerable<Track>> _queryGetTracksByPlaylistIdAsync =
        EF.CompileAsyncQuery((SoundWaveDbContext db, int id) => db.Playlists.Where(a => a.Id == id).SelectMany(t => t.Tracks!));

    public SoundWaveDbContext(DbContextOptions<SoundWaveDbContext> options)
        : base(options) => Interlocked.Increment(ref InstanceCount);

    public static long InstanceCount;

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddFilter(
            DbLoggerCategory.Database.Command.Name,
            LogLevel.Information);
    });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLoggerFactory(loggerFactory);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Album__97B4BE370AD2A005");

            entity.ToTable("Album");

            entity.HasIndex(e => e.ArtistId, "IFK_Artist_Album");

            entity.HasIndex(e => e.Id, "IPK_ProductItem");

            entity.Property(e => e.Title).HasMaxLength(160);

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Album__ArtistId__276EDEB3");

            entity.HasMany<Track>(e => e.Tracks)
                .WithOne(e => e.Album)
                .HasForeignKey(e => e.AlbumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Track__AlbumId__2B3F6F97");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Artist__25706B5007020F21");

            entity.ToTable("Artist");

            entity.HasIndex(e => e.Id, "IPK_Artist");

            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany<Album>(e => e.Albums)
                .WithOne(e => e.Artist)
                .HasForeignKey(e => e.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Album__ArtistId__276EDEB3");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__A4AE64D8164452B1");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.SupportRepId, "IFK_Employee_Customer");

            entity.HasIndex(e => e.Id, "IPK_Customer");

            entity.Property(e => e.Address).HasMaxLength(70);
            entity.Property(e => e.City).HasMaxLength(40);
            entity.Property(e => e.Company).HasMaxLength(80);
            entity.Property(e => e.Country).HasMaxLength(40);
            entity.Property(e => e.Email).HasMaxLength(60);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.FirstName).HasMaxLength(40);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.State).HasMaxLength(40);

            entity.HasOne(d => d.SupportRep).WithMany(p => p.Customers)
                .HasForeignKey(d => d.SupportRepId)
                .HasConstraintName("FK__Customer__Suppor__2C3393D0");

            entity.HasMany<Invoice>(e => e.Invoices)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Customer__2D27B809");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__7AD04F111273C1CD");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.ReportsTo, "IFK_Employee_ReportsTo");

            entity.HasIndex(e => e.Id, "IPK_Employee");

            entity.Property(e => e.Address).HasMaxLength(70);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(40);
            entity.Property(e => e.Country).HasMaxLength(40);
            entity.Property(e => e.Email).HasMaxLength(60);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.State).HasMaxLength(40);
            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo)
                .HasConstraintName("FK__Employee__Report__2B3F6F97");

            entity.HasMany<Customer>(e => e.Customers)
                .WithOne(e => e.SupportRep)
                .HasForeignKey(e => e.SupportRepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__Suppor__2C3393D0");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__0385057E7F60ED59");

            entity.ToTable("Genre");

            entity.HasIndex(e => e.Id, "IPK_Genre");

            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany<Track>(e => e.Tracks)
                .WithOne(e => e.Genre)
                .HasForeignKey(e => e.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Track__GenreId__2E1BDC42");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoice__D796AAB51A14E395");

            entity.ToTable("Invoice");

            entity.HasIndex(e => e.CustomerId, "IFK_Customer_Invoice");

            entity.HasIndex(e => e.Id, "IPK_Invoice");

            entity.Property(e => e.BillingAddress).HasMaxLength(70);
            entity.Property(e => e.BillingCity).HasMaxLength(40);
            entity.Property(e => e.BillingCountry).HasMaxLength(40);
            entity.Property(e => e.BillingPostalCode).HasMaxLength(10);
            entity.Property(e => e.BillingState).HasMaxLength(40);
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("numeric(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Custome__2D27B809");

            entity.HasMany<InvoiceLine>(e => e.InvoiceLines)
                .WithOne(e => e.Invoice)
                .HasForeignKey(e => e.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Invoi__2F10007B");
        });

        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InvoiceL__0D760AD91DE57479");

            entity.ToTable("InvoiceLine");

            entity.HasIndex(e => e.InvoiceId, "IFK_Invoice_InvoiceLine");

            entity.HasIndex(e => e.TrackId, "IFK_ProductItem_InvoiceLine");

            entity.HasIndex(e => e.Id, "IPK_InvoiceLine");

            entity.Property(e => e.UnitPrice).HasColumnType("numeric(10, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Invoi__2F10007B");

            entity.HasOne(d => d.Track).WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Track__2E1BDC42");
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MediaTyp__0E6FCB7203317E3D");

            entity.ToTable("MediaType");

            entity.HasIndex(e => e.Id, "IPK_MediaType");

            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany<Track>(e => e.Tracks)
                .WithOne(e => e.MediaType)
                .HasForeignKey(e => e.MediaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Track__MediaType__2D27B809");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__B30167A021B6055D");

            entity.ToTable("Playlist");

            entity.HasIndex(e => e.Id, "IPK_Playlist");

            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany(d => d.Tracks).WithMany(p => p.Playlists)
                .UsingEntity<Dictionary<string, object>>(
                    "PlaylistTrack",
                    r => r.HasOne<Track>().WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlaylistT__Track__300424B4"),
                    l => l.HasOne<Playlist>().WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlaylistT__Playl__30F848ED"),
                    j =>
                    {
                        j.HasKey("PlaylistId", "TrackId").HasName("PK__Playlist__A4A6282E25869641");
                        j.ToTable("PlaylistTrack");
                        j.HasIndex(new[] { "PlaylistId" }, "IFK_Playlist_PlaylistTrack");
                        j.HasIndex(new[] { "TrackId" }, "IFK_Track_PlaylistTrack");
                        j.HasIndex(new[] { "PlaylistId" }, "IPK_PlaylistTrack");
                    });
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Track__7A74F8E00EA330E9");

            entity.ToTable("Track");

            entity.HasIndex(e => e.AlbumId, "IFK_Album_Track");

            entity.HasIndex(e => e.GenreId, "IFK_Genre_Track");

            entity.HasIndex(e => e.MediaTypeId, "IFK_MediaType_Track");

            entity.HasIndex(e => e.Id, "IPK_Track");

            entity.Property(e => e.Composer).HasMaxLength(220);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UnitPrice).HasColumnType("numeric(10, 2)");

            entity.HasOne(d => d.Album).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__Track__AlbumId__286302EC");

            entity.HasOne(d => d.Genre).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Track__GenreId__2A4B4B5E");

            entity.HasOne(d => d.MediaType).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.MediaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Track__MediaType__29572725");

            entity.HasMany<InvoiceLine>(e => e.InvoiceLines)
                .WithOne(e => e.Track)
                .HasForeignKey(e => e.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Track__2E1BDC42");

            entity.HasMany<Playlist>(e => e.Playlists)
                .WithMany(p => p.Tracks);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
    }

    public bool AlbumExists(int id) => _queryAlbumExists(this, id);

    public IEnumerable<Album> GetAllAlbums() => _queryGetAllAlbums(this);

    public Album GetAlbum(int id) => _queryGetAlbum(this, id);

    public IEnumerable<Album> GetAlbumsByArtistId(int id) => _queryGetAlbumsByArtistId(this, id);

    public IEnumerable<Artist> GetAllArtists() => _queryGetAllArtists(this);

    public Artist GetArtist(int id) => _queryGetArtist(this, id);

    public IEnumerable<Customer> GetAllCustomers() => _queryGetAllCustomers(this);

    public Customer GetCustomer(int id) => _queryGetCustomer(this, id);

    public IEnumerable<Customer> GetCustomerBySupportRepId(int id) => _queryGetCustomerBySupportRepId(this, id);

    public IEnumerable<Employee> GetAllEmployees() => _queryGetAllEmployees(this);

    public Employee GetEmployee(int id) => _queryGetEmployee(this, id);

    public IEnumerable<Employee> GetEmployeeDirectReports(int id) => _queryGetDirectReports(this, id);

    public Employee GetEmployeeGetReportsTo(int id) => _queryGetReportsTo(this, id);

    public IEnumerable<Genre> GetAllGenres() => _queryGetAllGenres(this);

    public Genre GetGenre(int id) => _queryGetGenre(this, id);

    public IEnumerable<InvoiceLine> GetAllInvoiceLines() => _queryGetAllInvoiceLines(this);

    public InvoiceLine GetInvoiceLine(int id) => _queryGetInvoiceLine(this, id);

    public IEnumerable<InvoiceLine> GetInvoiceLinesByInvoiceId(int id) => _queryGetInvoiceLinesByInvoiceId(this, id);

    public IEnumerable<InvoiceLine> GetInvoiceLinesByTrackId(int id) => _queryGetInvoiceLinesByTrackId(this, id);

    public IEnumerable<Invoice> GetAllInvoices() => _queryGetAllInvoices(this);

    public Invoice GetInvoice(int id) => _queryGetInvoice(this, id);

    public IEnumerable<Invoice> GetInvoicesByCustomerId(int id) => _queryGetInvoicesByCustomerId(this, id);

    public IEnumerable<MediaType> GetAllMediaTypes() => _queryGetAllMediaTypes(this);

    public MediaType GetMediaType(int id) => _queryGetMediaType(this, id);

    public IEnumerable<Playlist> GetAllPlaylists() => _queryGetAllPlaylists(this);

    public Playlist GetPlaylist(int id) => _queryGetPlaylist(this, id);

    public IEnumerable<Playlist> GetPlaylistsByTrackId(int id) => _queryGetPlaylistsByTrackId(this, id);

    public IEnumerable<Track> GetAllTracks() => _queryGetAllTracks(this);

    public Track GetTrack(int id) => _queryGetTrack(this, id);

    public IEnumerable<Track> GetTracksByAlbumId(int id) => _queryGetTracksByAlbumId(this, id);

    public IEnumerable<Track> GetTracksByGenreId(int id) => _queryGetTracksByGenreId(this, id);

    public IEnumerable<Track> GetTracksByMediaTypeId(int id) => _queryGetTracksByMediaTypeId(this, id);

    public IEnumerable<Track> GetTracksByArtistId(int id) => _queryGetTracksByArtistId(this, id);

    public IEnumerable<Track> GetTracksByInvoiceId(int id) => _queryGetTracksByInvoiceId(this, id);

    public IEnumerable<Invoice> GetInvoicesByEmployeeId(int id) => _queryGetInvoicesByEmployeeId(this, id);

    public IEnumerable<Track> GetTracksByPlaylistId(int id) => _queryGetTracksByPlaylistId(this, id);

    public Task<bool> AlbumExistsAsync(int id) => _queryAlbumExistsAsync(this, id);

    public IAsyncEnumerable<Album> GetAllAlbumsAsync() => _queryGetAllAlbumsAsync(this);

    public Task<Album> GetAlbumAsync(int id) => _queryGetAlbumAsync(this, id);

    public IAsyncEnumerable<Album> GetAlbumsByArtistIdAsync(int id) => _queryGetAlbumsByArtistIdAsync(this, id);

    public IAsyncEnumerable<Artist> GetAllArtistsAsync() => _queryGetAllArtistsAsync(this);

    public Task<Artist> GetArtistAsync(int id) => _queryGetArtistAsync(this, id);

    public IAsyncEnumerable<Customer> GetAllCustomersAsync() => _queryGetAllCustomersAsync(this);

    public Task<Customer> GetCustomerAsync(int id) => _queryGetCustomerAsync(this, id);

    public IAsyncEnumerable<Customer> GetCustomerBySupportRepIdAsync(int id) => _queryGetCustomerBySupportRepIdAsync(this, id);

    public IAsyncEnumerable<Employee> GetAllEmployeesAsync() => _queryGetAllEmployeesAsync(this);

    public Task<Employee> GetEmployeeAsync(int id) => _queryGetEmployeeAsync(this, id);

    public IAsyncEnumerable<Employee> GetEmployeeDirectReportsAsync(int id) => _queryGetDirectReportsAsync(this, id);

    public Task<Employee> GetEmployeeGetReportsToAsync(int id) => _queryGetReportsToAsync(this, id);

    public IAsyncEnumerable<Genre> GetAllGenresAsync() => _queryGetAllGenresAsync(this);

    public Task<Genre> GetGenreAsync(int id) => _queryGetGenreAsync(this, id);

    public IAsyncEnumerable<InvoiceLine> GetAllInvoiceLinesAsync() => _queryGetAllInvoiceLinesAsync(this);

    public Task<InvoiceLine> GetInvoiceLineAsync(int id) => _queryGetInvoiceLineAsync(this, id);

    public IAsyncEnumerable<InvoiceLine> GetInvoiceLinesByInvoiceIdAsync(int id) => _queryGetInvoiceLinesByInvoiceIdAsync(this, id);

    public IAsyncEnumerable<InvoiceLine> GetInvoiceLinesByTrackIdAsync(int id) => _queryGetInvoiceLinesByTrackIdAsync(this, id);

    public IAsyncEnumerable<Invoice> GetAllInvoicesAsync() => _queryGetAllInvoicesAsync(this);

    public Task<Invoice> GetInvoiceAsync(int id) => _queryGetInvoiceAsync(this, id);

    public IAsyncEnumerable<Invoice> GetInvoicesByCustomerIdAsync(int id) => _queryGetInvoicesByCustomerIdAsync(this, id);

    public IAsyncEnumerable<MediaType> GetAllMediaTypesAsync() => _queryGetAllMediaTypesAsync(this);

    public Task<MediaType> GetMediaTypeAsync(int id) => _queryGetMediaTypeAsync(this, id);

    public IAsyncEnumerable<Playlist> GetAllPlaylistsAsync() => _queryGetAllPlaylistsAsync(this);

    public Task<Playlist> GetPlaylistAsync(int id) => _queryGetPlaylistAsync(this, id);

    public IAsyncEnumerable<Playlist> GetPlaylistsByTrackIdAsync(int id) => _queryGetPlaylistsByTrackIdAsync(this, id);

    public IAsyncEnumerable<Track> GetAllTracksAsync() => _queryGetAllTracksAsync(this);

    public Task<Track> GetTrackAsync(int id) => _queryGetTrackAsync(this, id);

    public IAsyncEnumerable<Track> GetTracksByAlbumIdAsync(int id) => _queryGetTracksByAlbumIdAsync(this, id);

    public IAsyncEnumerable<Track> GetTracksByGenreIdAsync(int id) => _queryGetTracksByGenreIdAsync(this, id);

    public IAsyncEnumerable<Track> GetTracksByMediaTypeIdAsync(int id) => _queryGetTracksByMediaTypeIdAsync(this, id);

    public IAsyncEnumerable<Track> GetTracksByArtistIdAsync(int id) => _queryGetTracksByArtistIdAsync(this, id);

    public IAsyncEnumerable<Track> GetTracksByInvoiceIdAsync(int id) => _queryGetTracksByInvoiceIdAsync(this, id);

    public IAsyncEnumerable<Invoice> GetInvoicesByEmployeeIdAsync(int id) => _queryGetInvoicesByEmployeeIdAsync(this, id);

    public IAsyncEnumerable<Track> GetTracksByPlaylistIdAsync(int id) => _queryGetTracksByPlaylistIdAsync(this, id);
}
