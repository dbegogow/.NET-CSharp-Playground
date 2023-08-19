using EFCoreCompiledQueries.Data.Models.Base;

namespace EFCoreCompiledQueries.Data.Models;

public class Playlist : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Track> Tracks { get; set; }
}
