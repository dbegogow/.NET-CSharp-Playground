using EFCoreCompiledQueries.Data.Models.Base;

namespace EFCoreCompiledQueries.Data.Models;

public class Artist : BaseEntity
{
    public Artist()
    {
        this.Albums = new HashSet<Album>();
    }

    public string Name { get; set; }

    public ICollection<Album> Albums { get; set; }
}
