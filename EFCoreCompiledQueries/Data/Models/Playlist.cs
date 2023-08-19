namespace EFCoreCompiledQueries.Data.Models;

public class Playlist
{
    public string Name { get; set; }

    public ICollection<Track> Tracks { get; set; }
}
