using EFCoreCompiledQueries.Data.Models.Base;

namespace EFCoreCompiledQueries.Data.Models;

public class InvoiceLine : BaseEntity
{
    public int InvoiceId { get; set; }

    public int TrackId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public Invoice Invoice { get; set; }

    public Track Track { get; set; }
}
