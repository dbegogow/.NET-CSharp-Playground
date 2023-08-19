using EFCoreCompiledQueries.Data.Models.Base;

namespace EFCoreCompiledQueries.Data.Models;

public class Album : BaseEntity
{
    public Album()
    {
        this.Tracks = new HashSet<Track>();
    }

    public string Title { get; set; }

    public int ArtistId { get; set; }

    public Artist Artist { get; set; }

    public ICollection<Track> Tracks { get; set; }
}
