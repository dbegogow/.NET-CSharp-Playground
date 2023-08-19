namespace EFCoreCompiledQueries.Data.Models;

public class MediaType
{
    public MediaType()
    {
        Tracks = new HashSet<Track>();
    }

    public string Name { get; set; }

    public ICollection<Track> Tracks { get; set; }
}
