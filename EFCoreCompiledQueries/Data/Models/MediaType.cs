using EFCoreCompiledQueries.Data.Models.Base;

namespace EFCoreCompiledQueries.Data.Models;

public class MediaType : BaseEntity
{
    public MediaType()
    {
        Tracks = new HashSet<Track>();
    }

    public string Name { get; set; }

    public ICollection<Track> Tracks { get; set; }
}
