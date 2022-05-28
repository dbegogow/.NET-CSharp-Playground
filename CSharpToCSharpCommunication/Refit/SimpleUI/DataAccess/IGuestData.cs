using SimpleUI.Models;
using Refit;

namespace SimpleUI.DataAccess;

public interface IGuestData
{
    [Get("/Guests")]
    Task<List<GuestModel>> GetGuests();
}
