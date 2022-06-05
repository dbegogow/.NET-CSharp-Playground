using RefitCommon;

using Refit;

namespace RefitClient;

public interface IPersonService
{
    [Get("/api/person")]
    Task<IEnumerable<Person>> GetAll();

    [Post("/api/person")]
    Task AddPerson([Body] Person person);
}
